using TournamentManagementLogic.Model;

namespace TournamentManagementLogic.Service.Interfaces;

public interface ITournamentService
{
    public List<TournamentBasicModel> GetTournamentList();

    public Guid CreateTournament(string name, string gameName, string description);

    public TournamentModel GetTournamentById(Guid id);

    public void DeleteTournament(Guid id);
}
