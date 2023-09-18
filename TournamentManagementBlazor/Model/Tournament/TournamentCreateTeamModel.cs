using System.ComponentModel.DataAnnotations;

namespace TournamentManagementBlazor.Model.Tournament;

public class TournamentCreateTeamModel
{
    [Required]
    [StringLength(50, MinimumLength = 3)]
    [Display(Name = "Team Name")]
    public string Name { get; set; } = string.Empty;
}