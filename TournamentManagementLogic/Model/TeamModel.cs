using System.ComponentModel.DataAnnotations;

namespace TournamentManagementLogic.Model;

public class TeamModel
{
    public Guid Id { get; init; } = Guid.NewGuid();

    [Display(Name = "Name")]
    public string Name { get; init; } = string.Empty;
}
