namespace TournamentManagementLogic.Model;

public class TournamentBasicModel
{
    public Guid Id { get; init; }

    public string Name { get; init; }

    public TournamentBasicModel()
    {
        Id = Guid.Empty;
        Name = string.Empty;
    }

    public override string ToString()
    {
        return Name;
    }
}
