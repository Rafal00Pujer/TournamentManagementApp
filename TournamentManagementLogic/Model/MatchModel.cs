namespace TournamentManagementLogic.Model;

public class MatchModel
{
    public Guid TournamentId { get; init; } = Guid.Empty;

    public Guid MatchId { get; init; } = Guid.Empty;

    public DateOnly? Date { get; init; }

    public TeamModel? FirstTeam { get; init; }

    public TeamModel? SecondTeam { get; init; }

    public TeamModel? Winner { get; init; }

    public IReadOnlyCollection<SetModel> Sets { get; init; } = new List<SetModel>();
}
