using System.ComponentModel.DataAnnotations;

namespace TournamentManagementLogic.Model;

public class SetModel
{
    public Guid Id { get; init; } = Guid.Empty;

    [Display(Name = "Set num")]
    public int SetNumber { get; init; }

    [Display(Name = "First Team Score")]
    public string FirstTeamScore { get; init; } = string.Empty;

    [Display(Name = "Second Team Score")]
    public string SecondTeamScore { get; init; } = string.Empty;
}
