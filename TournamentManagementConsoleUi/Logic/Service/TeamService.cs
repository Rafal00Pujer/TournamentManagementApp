using System.Collections;
using TournamentManagementConsoleUi.Logic.Database;
using TournamentManagementConsoleUi.Logic.Model;
using TournamentManagementConsoleUi.Logic.Service.Interfaces;

namespace TournamentManagementConsoleUi.Logic.Service;

internal class TeamService : ITeamService
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
        new TeamModel()
        {
            Id = t.Id,
            Name = t.Name
        });

        return teams.ToList();
    }

    public TeamModel GetTeam(Guid id)
    {
        var teamRecordsInDatabase = _database.GetTeamRecords();

        var teamRecord = teamRecordsInDatabase.First(t => t.Id == id);

        return new TeamModel()
        {
            Id = teamRecord.Id,
            Name = teamRecord.Name
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
