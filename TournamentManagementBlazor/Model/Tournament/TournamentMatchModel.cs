using System.ComponentModel.DataAnnotations;
using TournamentManagementLogic.Model;

namespace TournamentManagementBlazor.Model.Tournament;

public class TournamentMatchModel
{
    public Guid MatchId { get; init; } = Guid.Empty;

    [Display(Name = "Match date")]
    public DateOnly? Date { get; set; }

    public TeamModel? FirstTeam { get; init; }

    public TeamModel? SecondTeam { get; init; }

    public TeamModel? Winner { get; set; }

    [Display(Name = "Sets")]
    public IList<TournamentSetModel> Sets { get; init; } = new List<TournamentSetModel>();
}

