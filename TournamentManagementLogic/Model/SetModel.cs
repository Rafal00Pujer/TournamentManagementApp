using System.ComponentModel.DataAnnotations;

namespace TournamentManagementLogic.Model;

public class SetModel
{
    [Required]
    public Guid Id { get; init; } = Guid.Empty;

    [Required]
    [Display(Name = "Set num")]
    public int SetNumber { get; init; }

    [Required]
    [RegularExpression("[0-9]+")]
    [Display(Name = "First Team Score")]
    public string FirstTeamScore { get; set; } = "0";

    [Required]
    [RegularExpression("[0-9]+")]
    [Display(Name = "Second Team Score")]
    public string SecondTeamScore { get; set; } = "0";
}
