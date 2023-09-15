using System.ComponentModel.DataAnnotations;

namespace TournamentManagementLogic.Model;

public class TeamModel
{
    public Guid TournamentId { get; init; } = Guid.Empty;

    public Guid TeamId { get; init; } = Guid.Empty;

    public string TeamName { get; init; } = string.Empty;
}
