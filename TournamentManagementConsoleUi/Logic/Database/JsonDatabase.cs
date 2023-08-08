using System.Collections;
using System.Text.Json;

namespace TournamentManagementConsoleUi.Logic.Database;

public class JsonDatabase : IDatabase
{
    private const string DatabaseFolder = "Database/Json";

    public JsonDatabase()
    {
        var databasePath = Path.Combine(Directory.GetCurrentDirectory(), DatabaseFolder);

        if (!Directory.Exists(databasePath))
        {
            Directory.CreateDirectory(databasePath);
        }
    }

    #region Tournament Record

    private const string TournamentRecordFile = DatabaseFolder + "/Tournament.json";

    public void UpdateTournamentRecord(TournamentRecord tournamentRecord)
    {
        UpdateRecord(tournamentRecord, TournamentRecordFile);
    }

    public void DeleteTournamentRecord(TournamentRecord tournamentRecord)
    {
        DeleteRecord(tournamentRecord, TournamentRecordFile);
    }

    public List<TournamentRecord> GetTournamentRecords()
    {
        return GetAllRecordsFromFile<TournamentRecord>(TournamentRecordFile);
    }

    #endregion

    #region Team Record

    private const string TeamRecordFile = DatabaseFolder + "/Team.json";

    public void UpdateTeamRecord(TeamRecord teamRecord)
    {
        UpdateRecord(teamRecord, TeamRecordFile);
    }

    public void DeleteTeamRecord(TeamRecord teamRecord)
    {
        DeleteRecord(teamRecord, TeamRecordFile);
    }

    public List<TeamRecord> GetTeamRecords()
    {
        return GetAllRecordsFromFile<TeamRecord>(TeamRecordFile);
    }

    #endregion

    #region Match Record

    private const string MatchRecordFile = DatabaseFolder + "/Match.json";

    public void UpdateMatchRecord(MatchRecord matchRecord)
    {
        UpdateRecord(matchRecord, MatchRecordFile);
    }

    public void DeleteMatchRecord(MatchRecord matchRecord)
    {
        DeleteRecord(matchRecord, MatchRecordFile);
    }

    public List<MatchRecord> GetMatchRecords()
    {
        return GetAllRecordsFromFile<MatchRecord>(MatchRecordFile);
    }

    #endregion

    #region Set Record

    private const string SetRecordFile = DatabaseFolder + "/Set.json";

    public void UpdateSetRecord(SetRecord setRecord)
    {
        UpdateRecord(setRecord, SetRecordFile);
    }

    public void DeleteSetRecord(SetRecord setRecord)
    {
        DeleteRecord(setRecord, SetRecordFile);
    }

    public List<SetRecord> GetSetRecords()
    {
        return GetAllRecordsFromFile<SetRecord>(SetRecordFile);
    }

    #endregion

    private static void DeleteRecord<T>(T record, string fileName) where T : DatabaseRecord
    {
        var records = GetAllRecordsFromFile<T>(fileName);

        var recordInDatabase = records.Where(r => r.Id == record.Id).FirstOrDefault();

        if (recordInDatabase is not null)
        {
            records.Remove(recordInDatabase);

            SaveRecordsToFile<T>(records, fileName);
        }
    }

    private static void UpdateRecord<T>(T record, string fileName) where T : DatabaseRecord
    {
        var records = GetAllRecordsFromFile<T>(fileName);

        var recordInDatabase = records.Where(r => r.Id == record.Id).FirstOrDefault();

        if (recordInDatabase is not null)
        {
            records.Remove(recordInDatabase);
        }

        records.Add(record);

        SaveRecordsToFile<T>(records, fileName);
    }

    private static void SaveRecordsToFile<T>(IList records, string fileName)
    {
        var jsonString = JsonSerializer.Serialize(records);

        File.WriteAllText(fileName, jsonString);
    }

    private static List<T> GetAllRecordsFromFile<T>(string fileName)
    {
        List<T>? result = null;

        if (File.Exists(fileName))
        {
            var jsonString = File.ReadAllText(fileName);

            result = JsonSerializer.Deserialize<List<T>>(jsonString);
        }

        result ??= new();

        return result;
    }
}
