using TournamentManagementLogic.Model;

namespace TournamentManagementLogic.Service.Interfaces;

public interface IMatchService
{
    public IEnumerable<Guid> CreateMatchesForTournament(Guid tournamentId, IEnumerable<Guid> teamsIds);

    public MatchModel GetMatchById(Guid matchId);

    public void UpdateMatchDate(Guid matchId, DateOnly newDate);

    public void UpdateMatchWinner(Guid matchId, Guid winnerId);

    public void DeleteMatchesByTournamentId(Guid tournamentId);
}
