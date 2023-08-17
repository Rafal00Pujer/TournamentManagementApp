using System.ComponentModel.DataAnnotations;

namespace TournamentManagementLogic.Model;

public class MatchModel
{
    public Guid Id { get; init; } = Guid.Empty;

    [Display(Name = "Date")]
    public DateOnly? Date { get; init; }

    [Display(Name = "First Previous Match")]
    public MatchModel? FirstPreviousMatch { get; init; }

    [Display(Name = "Second Previous Match")]
    public MatchModel? SecondPreviousMatch { get; init; }

    [Display(Name = "First Team")]
    public TeamModel? FirstTeam { get; init; }

    [Display(Name = "Second Team")]
    public TeamModel? SecondTeam { get; init; }

    [Display(Name = "Winner")]
    public TeamModel? Winner { get; init; }

    [Display(Name = "Sets")]
    public List<SetModel> Sets { get; init; } = new();
}
