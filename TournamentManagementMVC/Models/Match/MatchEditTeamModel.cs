using System.ComponentModel.DataAnnotations;

namespace TournamentManagementMVC.Models.Match;

public class MatchEditTeamModel
{
    public Guid TeamId { get; set; } = Guid.Empty;

    [Display(Name = "Team Name")]
    public string TeamName { get; set; } = "-";

    public bool IsWinner { get; set; }
}

