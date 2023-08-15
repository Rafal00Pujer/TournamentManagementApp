using System.ComponentModel.DataAnnotations;

namespace TournamentManagementLogic.Model;

public class CreateTournamentModel
{
    [Display(Name = "Tournament Name")]
    [Required]
    [StringLength(50, MinimumLength = 3)]
    public string Name { get; set; } = string.Empty;

    [Display(Name = "Game Name")]
    [StringLength(50)]
    public string? GameName { get; set; }

    [Display(Name = "Tournament Description")]
    [StringLength(500)]
    public string? Description { get; set; }

    [Display(Name = "Teams")]
    [MinLength(1, ErrorMessage = "Add minimum one team.")]
    public List<TeamNameModel> Teams { get; set; } = new();
}