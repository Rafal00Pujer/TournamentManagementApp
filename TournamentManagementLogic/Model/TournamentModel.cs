using System.ComponentModel.DataAnnotations;

namespace TournamentManagementLogic.Model;

public class TournamentModel
{
    public Guid TournamentId { get; init; } = Guid.Empty;

    public string TournamentName { get; init; } = string.Empty;

    public string GameName { get; init; } = string.Empty;

    public string TournamentDescription { get; init; } = string.Empty;

    public IReadOnlyCollection<IReadOnlyCollection<MatchModel>> Rounds { get; init; } =
        new List<IReadOnlyCollection<MatchModel>>();
}
