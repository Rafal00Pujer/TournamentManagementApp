using System.ComponentModel.DataAnnotations;

namespace TournamentManagementLogic.Model;

public class MatchBasicModel
{
    [Required]
    public Guid Id { get; init; } = Guid.Empty;

    [Required]
    [Display(Name = "Date")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:O}", ApplyFormatInEditMode = true)]
    public DateOnly? Date { get; set; }

    public TeamModel? FirstTeam { get; init; }

    public TeamModel? SecondTeam { get; init; }

    public TeamModel? Winner { get; set; }

    [Display(Name = "Sets")]
    public List<SetModel> Sets { get; init; } = new();
}

