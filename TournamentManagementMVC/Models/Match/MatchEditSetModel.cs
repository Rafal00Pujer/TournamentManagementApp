using System.ComponentModel.DataAnnotations;

namespace TournamentManagementMVC.Models.Match;

public class MatchEditSetModel
{
    [Required]
    public Guid MatchId { get; set; } = Guid.Empty;

    [Required]
    public Guid SetId { get; set; } = Guid.Empty;

    [Required]
    [RegularExpression("[0-9]{1,3}$")]
    [Display(Name = "First Team Score")]
    public string FirstTeamScore { get; set; } = "0";

    [Required]
    [RegularExpression("[0-9]{1,3}$")]
    [Display(Name = "Second Team Score")]
    public string SecondTeamScore { get; set; } = "0";
}

