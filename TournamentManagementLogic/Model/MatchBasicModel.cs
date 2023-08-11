namespace TournamentManagementLogic.Model;

public class MatchBasicModel
{
    public Guid Id { get; init; } = Guid.Empty;

    public DateOnly? Date { get; set; }

    public Guid? FirstTeam { get; init; }

    public Guid? SecondTeam { get; init; }

    public Guid? Winner { get; set; }

    public void ClearWinner()
    {
        Winner = null;
    }

    public void SetWinner(int teamIndex)
    {
        switch (teamIndex)
        {
            case 0:
                Winner = FirstTeam;
                break;

            case 1:
                Winner = SecondTeam;
                break;

            default:
                ClearWinner();
                break;
        }
    }
}

