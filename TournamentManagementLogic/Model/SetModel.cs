using System.ComponentModel.DataAnnotations;

namespace TournamentManagementLogic.Model;

public class SetModel
{
    public Guid MatchId { get; init; } = Guid.Empty;

    public Guid SetId { get; init; } = Guid.Empty;

    public string FirstTeamScore { get; init; } = "0";

    public string SecondTeamScore { get; init; } = "0";
}
