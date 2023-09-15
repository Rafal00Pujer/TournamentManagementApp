using System.Collections;
using TournamentManagementLogic.Database;
using TournamentManagementLogic.Model;
using TournamentManagementLogic.Service.Interfaces;

namespace TournamentManagementLogic.Service;

public class TeamService : ITeamService
{
    private readonly IDatabase _database;

    public TeamService(IDatabase database)
    {
        _database = database;
    }

    public Guid CreateTeam(string name, Guid tournamentId)
    {
        var newTeamRecord = new TeamRecord(Guid.NewGuid(), name, tournamentId);

        _database.UpdateTeamRecord(newTeamRecord);

        return newTeamRecord.Id;
    }

    public IList GetTeamsForTournament(Guid tournamentId)
    {
        var teamRecordsInDatabase = _database.GetTeamRecords();

        var teamRecordsForTournament = teamRecordsInDatabase.Where(t => t.TournamentId == tournamentId);

        var teams = teamRecordsForTournament.Select(t =>
        new TeamModel
        {
            TournamentId = t.TournamentId,
            TeamId = t.Id,
            TeamName = t.Name
        });

        return teams.ToList();
    }

    public TeamModel GetTeam(Guid id)
    {
        var teamRecordsInDatabase = _database.GetTeamRecords();

        var teamRecord = teamRecordsInDatabase.First(t => t.Id == id);

        return new TeamModel
        {
            TournamentId = teamRecord.Id,
            TeamId = teamRecord.Id,
            TeamName = teamRecord.Name
        };
    }

    public void DeleteTeamsForTournament(Guid tournamentId)
    {
        var teamRecordsInDatabase = _database.GetTeamRecords();

        var teamRecordsForTournament = teamRecordsInDatabase.Where(t => t.TournamentId == tournamentId);

        foreach (var teamRecord in teamRecordsForTournament)
        {
            _database.DeleteTeamRecord(teamRecord);
        }
    }
}
