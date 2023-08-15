using TournamentManagementLogic.Database;
using TournamentManagementLogic.Model;
using TournamentManagementLogic.Service.Interfaces;

namespace TournamentManagementLogic.Service;

public class TournamentService : ITournamentService
{
    private readonly IDatabase _database;

    public TournamentService(IDatabase database)
    {
        _database = database;
    }

    public Guid CreateTournament(CreateTournamentModel model)
    {
        var newTournamentRecord = new TournamentRecord(Guid.NewGuid(), model.Name, model.GameName ?? string.Empty, model.Description ?? string.Empty);

        var newTournamentId = newTournamentRecord.Id;

        _database.UpdateTournamentRecord(newTournamentRecord);

        var teamsIds = new List<Guid>();

        foreach (var newTeamRecord in model.Teams.Select(teamNameModel => new TeamRecord(Guid.NewGuid(), teamNameModel.Name, newTournamentId)))
        {
            teamsIds.Add(newTeamRecord.Id);
            _database.UpdateTeamRecord(newTeamRecord);
        }

        CreateMatch(teamsIds, newTournamentId);

        return newTournamentId;
    }

    private Guid CreateMatch(IReadOnlyList<Guid> availableTeamIds, Guid tournamentId)
    {
        MatchRecord newMatchRecord;

        if (availableTeamIds.Count > 2)
        {
            var teamsAreEven = availableTeamIds.Count % 2 == 0;
            var halfCount = availableTeamIds.Count / 2;

            var firstHalfOfTeamIds = availableTeamIds.Take(halfCount).ToList();
            var secondHalfOfTeamIds = availableTeamIds.TakeLast(halfCount + (teamsAreEven ? 0 : 1)).ToList();

            Guid? firstTeamId = null;
            Guid? firstMatchId = null;

            if (firstHalfOfTeamIds.Count == 1)
            {
                firstTeamId = firstHalfOfTeamIds[0];
            }
            else
            {
                firstMatchId = CreateMatch(firstHalfOfTeamIds, tournamentId);
            }

            var secondMatchId = CreateMatch(secondHalfOfTeamIds, tournamentId);

            newMatchRecord = new MatchRecord(Guid.NewGuid(), tournamentId, null, firstMatchId, secondMatchId, firstTeamId, null, null);
        }
        else
        {
            var firstTeamId = availableTeamIds[0];
            Guid? secondTeamId = null;

            if (availableTeamIds.Count > 1)
            {
                secondTeamId = availableTeamIds[1];
            }

            newMatchRecord = new MatchRecord(Guid.NewGuid(), tournamentId, null, null, null, firstTeamId, secondTeamId, null);
        }

        _database.UpdateMatchRecord(newMatchRecord);

        return newMatchRecord.Id;
    }

    public List<TournamentBasicModel> GetTournamentList()
    {
        var tournamentRecords = _database.GetTournamentRecords();

        var tournamentsBasic = tournamentRecords.Select(tournament =>
        new TournamentBasicModel()
        {
            Id = tournament.Id,
            Name = tournament.Name,
            GameName = tournament.GameName,
            Description = tournament.Description
        });

        return tournamentsBasic.ToList();
    }

    public TournamentBasicModel GetTournamentBasicById(Guid id)
    {
        var tournamentRecords = _database.GetTournamentRecords();

        var tournament = tournamentRecords.FirstOrDefault(t => t.Id == id);

        if (tournament is not null)
        {
            return new TournamentBasicModel
            {
                Id = tournament.Id,
                Name = tournament.Name,
                GameName = tournament.GameName,
                Description = tournament.Description
            };
        }

        return new TournamentBasicModel();
    }

    public TournamentModel GetTournamentById(Guid id)
    {
        var tournamentRecords = _database.GetTournamentRecords();

        var tournament = tournamentRecords.FirstOrDefault(t => t.Id == id);

        if (tournament is not null)
        {
            return new TournamentModel
            {
                Id = tournament.Id,
                Name = tournament.Name,
                GameName = tournament.GameName,
                Description = tournament.Description
            };
        }

        return new TournamentModel();
    }

    public void DeleteTournament(Guid id)
    {
        var tournamentRecord = _database.GetTournamentRecords().FirstOrDefault(t => t.Id == id);

        if (tournamentRecord is null)
        {
            return;
        }

        // delete matches
        var matchRecords = _database.GetMatchRecords().Where(m => m.TournamentId == id);
        foreach (var matchRecord in matchRecords)
        {
            _database.DeleteMatchRecord(matchRecord);

            // delete sets
            var setRecords = _database.GetSetRecords().Where(s => s.MatchId == matchRecord.Id);
            foreach (var setRecord in setRecords)
            {
                _database.DeleteSetRecord(setRecord);
            }
        }

        // delete teams
        var teamRecords = _database.GetTeamRecords().Where(t => t.TournamentId == id);
        foreach (var teamRecord in teamRecords)
        {
            _database.DeleteTeamRecord(teamRecord);
        }

        _database.DeleteTournamentRecord(tournamentRecord);
    }
}
