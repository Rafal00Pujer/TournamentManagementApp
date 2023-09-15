using System.ComponentModel.DataAnnotations;
using TournamentManagementLogic.Model;

namespace TournamentManagementMVC.Models.Match;

public class MatchEditModel
{
    public Guid TournamentId { get; set; } = Guid.Empty;

    [Required]
    public Guid MatchId { get; set; } = Guid.Empty;

    [Required]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:O}", ApplyFormatInEditMode = true)]
    [Display(Name = "Date")]
    public DateOnly? Date { get; set; }

    [Display(Name = "First Team")]
    public MatchEditTeamModel FirstTeam { get; set; } = new();

    [Display(Name = "Second Team")]
    public MatchEditTeamModel SecondTeam { get; set; } = new();

    [Display(Name = "Sets")]
    public ICollection<MatchEditSetModel> Sets { get; set; } = new List<MatchEditSetModel>();
}
