using System.ComponentModel.DataAnnotations;

namespace TournamentManagementMVC.Models.Tournament.Open;

public class TournamentOpenMatchModel
{
    public Guid MatchId { get; set; } = Guid.Empty;

    [Display(Name = "Match Date")]
    public DateOnly? MatchDate { get; set; }

    public TournamentOpenTeamModel[] MatchTeams { get; set; } = new TournamentOpenTeamModel[2];
}

