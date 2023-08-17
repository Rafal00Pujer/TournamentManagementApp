using System.ComponentModel.DataAnnotations;

namespace TournamentManagementLogic.Model;

public class TournamentModel
{
    public Guid Id { get; init; } = Guid.Empty;

    [Display(Name = "Name")]
    public string Name { get; init; } = string.Empty;

    [Display(Name = "Game Name")]
    public string GameName { get; init; } = string.Empty;

    [Display(Name = "Description")]
    public string Description { get; init; } = string.Empty;

    [Display(Name = "Final Match")]
    public MatchModel FinalMatch { get; set; } = new MatchModel();

    [Display(Name = "Matches")]
    public List<MatchModel> Matches { get; init; } = new();
}
