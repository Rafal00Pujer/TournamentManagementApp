namespace TournamentManagementConsoleUi.Logic.Model;

public class TeamModel
{
    public Guid Id { get; init; }

    public string Name { get; init; }

    public TeamModel()
    {
        Id = Guid.Empty;
        Name = string.Empty;
    }

    public override string ToString()
    {
        return Name;
    }
}
