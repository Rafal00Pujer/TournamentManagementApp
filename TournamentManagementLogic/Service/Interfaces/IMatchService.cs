using TournamentManagementLogic.Model;

namespace TournamentManagementLogic.Service.Interfaces;

public interface IMatchService
{
    public Guid CreateMatch(Guid tournamentId, Guid? firstPreviousMatch, Guid? secondPreviousMatch, Guid? firstTeam, Guid? secondTeam);

    public List<MatchWithDependencyModel> GetMatchesForTournament(Guid tournamentId);

    public void DeleteMatchesForTournament(Guid tournamentId);

    public MatchBasicModel GetMatchById(Guid matchId);

    public void UpdateMatchDate(MatchBasicModel match);

    public void UpdateMatchWinner(MatchBasicModel match);
}
