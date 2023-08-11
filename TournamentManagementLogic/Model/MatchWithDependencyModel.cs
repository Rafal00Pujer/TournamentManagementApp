namespace TournamentManagementLogic.Model;

public class MatchWithDependencyModel
{
    public Guid Id { get; init; } = Guid.Empty;

    public DateOnly? Date { get; init; }

    public MatchWithDependencyModel? FirstPreviousMatch { get; init; }

    public MatchWithDependencyModel? SecondPreviousMatch { get; init; }

    public Guid? FirstTeam { get; init; }

    public Guid? SecondTeam { get; init; }

    public Guid? Winner { get; init; }
}
