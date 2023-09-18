using System.ComponentModel.DataAnnotations;

namespace TournamentManagementBlazor.Model.Tournament;

public class TournamentCreateModel
{
    [Display(Name = "Tournament Name")]
    [Required]
    [StringLength(50, MinimumLength = 3)]
    public string TournamentName { get; set; } = string.Empty;

    [Display(Name = "Game Name")]
    [StringLength(50)]
    public string? GameName { get; set; }

    [Display(Name = "Tournament Description")]
    [StringLength(500)]
    public string? TournamentDescription { get; set; }

    [Display(Name = "Teams")]
    [Required]
    [ValidateComplexType]
    [MinLength(1, ErrorMessage = "Add minimum one team.")]
    public IList<TournamentCreateTeamModel> Teams { get; set; } = new List<TournamentCreateTeamModel> { new() };
}