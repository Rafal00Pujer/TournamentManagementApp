namespace TournamentManagementConsoleUi.Logic.Database;

public interface IDatabase
{
    public void UpdateTournamentRecord(TournamentRecord tournamentRecord);

    public void DeleteTournamentRecord(TournamentRecord tournamentRecord);

    public List<TournamentRecord> GetTournamentRecords();

    public void UpdateTeamRecord(TeamRecord teamRecord);

    public void DeleteTeamRecord(TeamRecord teamRecord);

    public List<TeamRecord> GetTeamRecords();

    public void UpdateMatchRecord(MatchRecord matchRecord);

    public void DeleteMatchRecord(MatchRecord matchRecord);

    public List<MatchRecord> GetMatchRecords();

    public void UpdateSetRecord(SetRecord setRecord);

    public void DeleteSetRecord(SetRecord setRecord);

    public List<SetRecord> GetSetRecords();
}
