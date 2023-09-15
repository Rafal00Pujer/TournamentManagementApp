using TournamentManagementLogic.Model;

namespace TournamentManagementLogic.Service.Interfaces;

public interface ITournamentRoundsService
{
    public IReadOnlyCollection<IReadOnlyCollection<MatchModel>> GetTournamentRounds(Guid tournamentId);
}

