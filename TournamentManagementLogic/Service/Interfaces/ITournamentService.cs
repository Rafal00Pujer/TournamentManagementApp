using TournamentManagementLogic.Model;

namespace TournamentManagementLogic.Service.Interfaces;

public interface ITournamentService
{
    public IEnumerable<TournamentModel> GetTournaments();

    public Guid CreateTournament(string tournamentName, string? gameName, string? tournamentDescription, IEnumerable<string> teamsNames);

    public TournamentModel GetTournamentById(Guid id);

    public void DeleteTournament(Guid id);
}
