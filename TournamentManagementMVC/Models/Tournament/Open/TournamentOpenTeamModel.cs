using System.ComponentModel.DataAnnotations;

namespace TournamentManagementMVC.Models.Tournament.Open;

public class TournamentOpenTeamModel
{
    [Display(Name = "Team Name")]
    public string TeamName { get; set; } = "-";

    public bool IsWinner { get; set; }

    [Display(Name = "Team Sets Scores")]
    public string[] TeamSetsScores { get; set; } = { string.Empty, string.Empty, string.Empty };
}

