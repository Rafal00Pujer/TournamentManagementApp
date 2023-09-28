using System.ComponentModel.DataAnnotations;

namespace TournamentManagementBlazor.Model.Tournament;

public class TournamentSetModel
{
    public Guid SetId { get; init; } = Guid.Empty;

    [Required]
    [RegularExpression("[0-9]{1,3}$", ErrorMessage = "The score must be between 0 and 999.")]
    [Display(Name = "First team score")]
    public string FirstTeamScore { get; set; } = "0";

    [Required]
    [RegularExpression("[0-9]{1,3}$", ErrorMessage = "The score must be between 0 and 999.")]
    [Display(Name = "Second team score")]
    public string SecondTeamScore { get; set; } = "0";
}

