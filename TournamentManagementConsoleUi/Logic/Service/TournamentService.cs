using System.Text.RegularExpressions;
using TournamentManagementConsoleUi.Logic.Database;
using TournamentManagementConsoleUi.Logic.Model;
using TournamentManagementConsoleUi.Logic.Service.Interfaces;

namespace TournamentManagementConsoleUi.Logic.Service;

internal class TournamentService : ITournamentService
{
    private readonly IDatabase _database;

    public TournamentService(IDatabase database)
    {
        _database = database;
    }

    public Guid CreateTournament(string name, string gameName, string description)
    {
        var newTournamentRecord = new TournamentRecord(Guid.NewGuid(), name, gameName, description);

        _database.UpdateTournamentRecord(newTournamentRecord);

        return newTournamentRecord.Id;
    }

    public List<TournamentBasicModel> GetTournamentList()
    {
        var tournamentRecords = _database.GetTournamentRecords();

        var tournamentsBasic = tournamentRecords.Select(tournament =>
        new TournamentBasicModel()
        {
            Id = tournament.Id,
            Name = tournament.Name
        });

        return tournamentsBasic.ToList();
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

        if (tournamentRecord is not null)
        {
            _database.DeleteTournamentRecord(tournamentRecord);
        }
    }
}
