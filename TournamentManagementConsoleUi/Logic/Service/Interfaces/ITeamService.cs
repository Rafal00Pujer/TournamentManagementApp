using System.Collections;
using TournamentManagementConsoleUi.Logic.Model;

namespace TournamentManagementConsoleUi.Logic.Service.Interfaces;

public interface ITeamService
{
    public Guid CreateTeam(string name, Guid tournamentId);

    public IList GetTeamsForTournament(Guid tournamentId);

    public TeamModel GetTeam(Guid id);

    public void DeleteTeamsForTournament(Guid tournamentId);
}
