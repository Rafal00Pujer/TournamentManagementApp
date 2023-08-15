using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace TournamentManagementLogic.Model;

public class TeamNameModel
{
    [Display(Name = "Team Name"), Required, StringLength(50, MinimumLength = 3)]
    public string Name { get; init; }
}