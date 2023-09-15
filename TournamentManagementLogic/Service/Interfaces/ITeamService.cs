using TournamentManagementLogic.Model;

namespace TournamentManagementLogic.Service.Interfaces;

public interface ITeamService
{
    public Guid CreateTeam(string name, Guid tournamentId);

    public TeamModel GetTeam(Guid id);

    public void DeleteTeamsForTournament(Guid tournamentId);
}