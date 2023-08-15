using TournamentManagementLogic.Model;

namespace TournamentManagementLogic.Service.Interfaces;

public interface ITournamentService
{
    public List<TournamentBasicModel> GetTournamentList();

    public Guid CreateTournament(CreateTournamentModel model);

    public TournamentBasicModel GetTournamentBasicById(Guid id);

    public TournamentModel GetTournamentById(Guid id);

    public void DeleteTournament(Guid id);
}
