using System.ComponentModel.DataAnnotations;

namespace TournamentManagementMVC.Models.Tournament.Create;

public class TournamentCreateTeamModel
{
    [Required]
    [StringLength(50, MinimumLength = 3)]
    [Display(Name = "Team Name")]
    public string Name { get; set; } = string.Empty;
}