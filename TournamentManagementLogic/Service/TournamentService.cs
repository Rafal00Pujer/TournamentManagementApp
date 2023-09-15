using TournamentManagementLogic.Database;
using TournamentManagementLogic.Model;
using TournamentManagementLogic.Service.Interfaces;

namespace TournamentManagementLogic.Service;

public class TournamentService : ITournamentService
{
    private readonly IDatabase _database;
    private readonly ITournamentRoundsService _tournamentRoundsService;
    private readonly IMatchService _matchService;
    private readonly ITeamService _teamService;

    public TournamentService(IDatabase database, ITournamentRoundsService tournamentRoundsService, IMatchService matchService, ITeamService teamService)
    {
        _database = database;
        _tournamentRoundsService = tournamentRoundsService;
        _matchService = matchService;
        _teamService = teamService;
    }

    public IEnumerable<TournamentModel> GetTournaments()
    {
        var tournaments = _database.GetTournamentRecords()
            .Select(t => new TournamentModel
            {
                TournamentId = t.Id,
                TournamentName = t.Name,
                GameName = t.GameName,
                TournamentDescription = t.Description,
                Rounds = _tournamentRoundsService.GetTournamentRounds(t.Id)
            });

        return tournaments;
    }

    public Guid CreateTournament(string tournamentName, string? gameName, string? tournamentDescription, IEnumerable<string> teamsNames)
    {
        var newTournamentRecord = new TournamentRecord(Guid.NewGuid(), tournamentName, gameName ?? string.Empty, tournamentDescription ?? string.Empty);

        var newTournamentId = newTournamentRecord.Id;

        _database.UpdateTournamentRecord(newTournamentRecord);

        var teamsIds = teamsNames.Select(teamName => _teamService.CreateTeam(teamName, newTournamentId));

        _ = _matchService.CreateMatchesForTournament(newTournamentId, teamsIds);

        return newTournamentId;
    }

    public TournamentModel GetTournamentById(Guid id)
    {
        var tournamentRecord = _database.GetTournamentRecords().FirstOrDefault(t => t.Id == id);

        if (tournamentRecord is null)
        {
            return new TournamentModel();
        }

        var tournament = new TournamentModel
        {
            TournamentId = tournamentRecord.Id,
            TournamentName = tournamentRecord.Name,
            GameName = tournamentRecord.GameName,
            TournamentDescription = tournamentRecord.Description,
            Rounds = _tournamentRoundsService.GetTournamentRounds(tournamentRecord.Id)
        };

        return tournament;
    }

    public void DeleteTournament(Guid id)
    {
        var tournamentRecord = _database.GetTournamentRecords().FirstOrDefault(t => t.Id == id);

        if (tournamentRecord is null)
        {
            return;
        }

        _matchService.DeleteMatchesByTournamentId(tournamentRecord.Id);
        _teamService.DeleteTeamsForTournament(tournamentRecord.Id);
        _database.DeleteTournamentRecord(tournamentRecord);
    }
}
