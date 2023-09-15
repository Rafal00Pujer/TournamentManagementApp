using System.ComponentModel.DataAnnotations;

namespace TournamentManagementLogic.Model;

public class SetModel
{
    public Guid MatchId { get; set; } = Guid.Empty;

    public Guid SetId { get; init; } = Guid.Empty;

    public string FirstTeamScore { get; set; } = "0";

    public string SecondTeamScore { get; set; } = "0";
}
