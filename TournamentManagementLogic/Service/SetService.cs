using TournamentManagementLogic.Database;
using TournamentManagementLogic.Model;
using TournamentManagementLogic.Service.Interfaces;

namespace TournamentManagementLogic.Service;

public class SetService : ISetService
{
    private readonly IDatabase _database;

    public SetService(IDatabase database)
    {
        _database = database;
    }

    public IEnumerable<SetModel> GetSetsForMatch(Guid matchId)
    {
        var setModels = _database.GetSetRecords()
            .Where(s => s.MatchId == matchId)
            .OrderBy(s => s.SetNumber)
            .Select(s => new SetModel
            {
                MatchId = s.MatchId,
                SetId = s.Id,
                FirstTeamScore = s.FirstTeamScore,
                SecondTeamScore = s.SecondTeamScore
            });

        return setModels;
    }

    public void DeleteSetsForMatch(Guid matchId)
    {
        var setRecords = _database.GetSetRecords().Where(s => s.MatchId == matchId);

        foreach (var setRecord in setRecords)
        {
            _database.DeleteSetRecord(setRecord);
        }
    }

    public Guid CreateEmptySet(Guid matchId)
    {
        var setRecordsForMatch = _database.GetSetRecords()
            .Where(s => s.MatchId == matchId)
            .ToArray();

        var nextSetNum = 1;

        if (setRecordsForMatch.Length > 0)
        {
            nextSetNum = setRecordsForMatch.Max(s => s.SetNumber) + 1;
        }

        var newSetRecord = new SetRecord(Guid.NewGuid(), matchId, nextSetNum, string.Empty, string.Empty);

        _database.UpdateSetRecord(newSetRecord);

        return newSetRecord.Id;
    }

    public SetModel GetSet(Guid id)
    {
        var setRecord = _database.GetSetRecords().FirstOrDefault(s => s.Id == id);

        if (setRecord is null)
        {
            return new SetModel();
        }

        return new SetModel
        {
            MatchId = setRecord.MatchId,
            SetId = setRecord.Id,
            FirstTeamScore = setRecord.FirstTeamScore,
            SecondTeamScore = setRecord.SecondTeamScore
        };
    }

    public void DeleteSet(Guid id)
    {
        var setRecord = _database.GetSetRecords().FirstOrDefault(s => s.Id == id);

        if (setRecord is not null)
        {
            _database.DeleteSetRecord(setRecord);
        }
    }

    public void UpdateScores(Guid setId, string firstTeamScore, string secondTeamScore)
    {
        var setRecord = _database.GetSetRecords().FirstOrDefault(s => s.Id == setId);

        if (setRecord is null)
        {
            return;
        }

        setRecord = setRecord with
        {
            FirstTeamScore = firstTeamScore,
            SecondTeamScore = secondTeamScore
        };

        _database.UpdateSetRecord(setRecord);
    }
}
