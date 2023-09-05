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

    public List<SetModel> GetSetsForMatch(Guid matchId)
    {
        var setRecords = _database.GetSetRecords().Where(s => s.MatchId == matchId);

        var setModels = setRecords.Select(m => new SetModel()
        {
            Id = m.Id,
            SetNumber = m.SetNumber,
            FirstTeamScore = m.FirstTeamScore,
            SecondTeamScore = m.SecondTeamScore
        });

        return setModels.ToList();
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
        var setRecordsForMatch = _database.GetSetRecords().Where(s => s.MatchId == matchId);

        var nextSetNum = 1;

        if (setRecordsForMatch.Any())
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
            Id = setRecord.Id,
            SetNumber = setRecord.SetNumber,
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

    public void UpdateScores(SetModel model)
    {
        var setRecord = _database.GetSetRecords().FirstOrDefault(s => s.Id == model.Id);

        if (setRecord is null)
        {
            return;
        }

        setRecord = setRecord with
        {
            FirstTeamScore = model.FirstTeamScore,
            SecondTeamScore = model.SecondTeamScore
        };

        _database.UpdateSetRecord(setRecord);
    }
}
