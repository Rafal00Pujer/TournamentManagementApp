using System.ComponentModel.DataAnnotations;

namespace TournamentManagementMVC.Models.Tournament.Open;

public class TournamentOpenRoundModel
{
    [Display(Name = "Round Name")]
    public string RoundName { get; set; }

    [Display(Name = "Round Matches")]
    public IReadOnlyCollection<TournamentOpenMatchModel> RoundMatches { get; set; } = new List<TournamentOpenMatchModel>();
}

