using TournamentManagementConsoleUi.Logic.Model;

namespace TournamentManagementConsoleUi.Logic.Service.Interfaces;

public interface ISetService
{
    public List<SetModel> GetSetsForMatch(Guid matchId);

    public void DeleteSetsForMatch(Guid matchId);

    public Guid CreateEmptySet(Guid matchId);

    public SetModel GetSet(Guid id);

    public void DeleteSet(Guid id);

    public void UpdateScores(SetModel model);
}
