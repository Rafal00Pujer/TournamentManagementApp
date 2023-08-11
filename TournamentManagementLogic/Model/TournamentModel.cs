namespace TournamentManagementLogic.Model;

public class TournamentModel
{
    public Guid Id { get; init; } = Guid.Empty;

    public string Name { get; init; } = string.Empty;

    public string GameName { get; init; } = string.Empty;

    public string Description { get; init; } = string.Empty;

    public override string ToString()
    {
        return Name;
    }
}
