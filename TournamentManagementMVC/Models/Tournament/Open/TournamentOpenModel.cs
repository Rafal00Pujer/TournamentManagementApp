using System.ComponentModel.DataAnnotations;

namespace TournamentManagementMVC.Models.Tournament.Open;

public class TournamentOpenModel
{
    public Guid TournamentId { get; set; } = Guid.Empty;

    [Display(Name = "Tournament Name")]
    public string TournamentName { get; set; } = string.Empty;

    [Display(Name = "Game Name")]
    public string GameName { get; set; } = string.Empty;

    [Display(Name = "Tournament Description")]
    public string TournamentDescription { get; set; } = string.Empty;

    [Display(Name = "Rounds")]
    public IReadOnlyCollection<TournamentOpenRoundModel> Rounds { get; set; } = new List<TournamentOpenRoundModel>();
}

