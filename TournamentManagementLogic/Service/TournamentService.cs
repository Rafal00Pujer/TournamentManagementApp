using System.Text.RegularExpressions;
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

        var tournamentRecord = tournamentRecords.FirstOrDefault(t => t.Id == id);

        if (tournamentRecord is null)
        {
            return new TournamentModel();
        }

        var teamModels = _database.GetTeamRecords().Where(t => t.TournamentId == id).Select(t => new TeamModel { Id = t.Id, Name = t.Name }).ToList();

        var matchRecords = _database.GetMatchRecords().Where(m => m.TournamentId == id).ToList();

        var finalMatchRecord = GetFinalMatch(matchRecords);

        var matchModels = new List<MatchModel>();

        var finalMatchModel = CreateMatchModelRecursive(matchRecords, teamModels, finalMatchRecord, matchModels);

        return new TournamentModel
        {
            Id = tournamentRecord.Id,
            Name = tournamentRecord.Name,
            GameName = tournamentRecord.GameName,
            Description = tournamentRecord.Description,
            FinalMatch = finalMatchModel,
            Matches = matchModels
        };
    }

    private static MatchRecord GetFinalMatch(IReadOnlyList<MatchRecord> matchRecords) =>
        matchRecords.Count switch
        {
            1 => matchRecords[0],
            2 => matchRecords.First(m => m.SecondPreviousMach is not null),
            _ => matchRecords.First(m1 =>
                m1.FirstPreviousMach is not null && m1.SecondPreviousMach is not null && matchRecords
                    .Where(m2 => m2 != m1)
                    .All(m2 => m2.FirstPreviousMach != m1.Id && m2.SecondPreviousMach != m1.Id))
        };

    private MatchModel CreateMatchModelRecursive(IReadOnlyList<MatchRecord> matchRecords, IReadOnlyList<TeamModel> teams, MatchRecord record, ICollection<MatchModel> matchModels)
    {
        var firstPreviousMatch = GetPreviousMatchModel(record.FirstPreviousMach);
        var secondPreviousMatch = GetPreviousMatchModel(record.SecondPreviousMach);

        var firstTeam = GetTeam(record.FirstTeam);
        var secondTeam = GetTeam(record.SecondTeam);

        TeamModel? winner = null;
        if (record.Winner is not null)
        {
            if (record.Winner == record.FirstTeam)
            {
                winner = firstTeam;
            }
            else if (record.Winner == record.SecondTeam)
            {
                winner = secondTeam;
            }
        }

        var sets = _database.GetSetRecords().Where(s => s.MatchId == record.Id).Select(s => new SetModel
        {
            Id = s.Id,
            SetNumber = s.SetNumber,
            FirstTeamScore = s.FirstTeamScore,
            SecondTeamScore = s.SecondTeamScore,
        }).ToList();

        var matchModel = new MatchModel
        {
            Id = record.Id,
            Date = record.Date,
            FirstPreviousMatch = firstPreviousMatch,
            SecondPreviousMatch = secondPreviousMatch,
            FirstTeam = firstTeam,
            SecondTeam = secondTeam,
            Winner = winner,
            Sets = sets
        };

        matchModels.Add(matchModel);

        return matchModel;

        MatchModel? GetPreviousMatchModel(Guid? previousMatchId)
        {
            if (previousMatchId is null)
            {
                return null;
            }

            var previousMatchRecord = matchRecords.First(m => m.Id == previousMatchId);

            return CreateMatchModelRecursive(matchRecords, teams, previousMatchRecord, matchModels);
        }

        TeamModel? GetTeam(Guid? teamId)
        {
            return teamId is null ? null : teams.First(t => t.Id == teamId);
        }
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
