using TournamentManagementLogic.Model;

namespace TournamentManagementLogic.Service.Interfaces;

public interface ISetService
{
    public IEnumerable<SetModel> GetSetsForMatch(Guid matchId);

    public void DeleteSetsForMatch(Guid matchId);

    public Guid CreateEmptySet(Guid matchId);

    public SetModel GetSet(Guid id);

    public void DeleteSet(Guid id);

    public void UpdateScores(Guid setId, string firstTeamScore, string secondTeamScore);
}