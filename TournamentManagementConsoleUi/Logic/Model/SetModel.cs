namespace TournamentManagementConsoleUi.Logic.Model;

public class SetModel
{
    public Guid Id { get; init; } = Guid.Empty;

    public int SetNumber { get; init; }

    public int FirstTeamScore { get; set; }

    public int SecondTeamScore { get; set; }
}
