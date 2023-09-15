using TournamentManagementLogic.Database;
using TournamentManagementLogic.Model;
using TournamentManagementLogic.Service.Interfaces;

namespace TournamentManagementLogic.Service;

public class MatchService : IMatchService
{
    private readonly IDatabase _database;
    private readonly ITeamService _teamService;
    private readonly ISetService _setService;

    public MatchService(IDatabase database, ITeamService teamService, ISetService setService)
    {
        _database = database;
        _teamService = teamService;
        _setService = setService;
    }

    public IEnumerable<Guid> CreateMatchesForTournament(Guid tournamentId, IEnumerable<Guid> teamsIds)
    {
        var newMatchesIds = new List<Guid>();

        _ = CreateMatchRecursive(teamsIds.ToArray(), tournamentId, newMatchesIds);

        return newMatchesIds;
    }

    public MatchModel GetMatchById(Guid matchId)
    {
        var matchRecord = _database.GetMatchRecords().FirstOrDefault(m => m.Id == matchId);

        if (matchRecord is null)
        {
            return new MatchModel();
        }

        GetTeamsAndWinner(matchRecord, out var firstTeam, out var secondTeam, out var winner);

        var sets = _setService.GetSetsForMatch(matchRecord.Id);

        return new MatchModel
        {
            TournamentId = matchRecord.TournamentId,
            MatchId = matchRecord.Id,
            Date = matchRecord.Date,
            FirstTeam = firstTeam,
            SecondTeam = secondTeam,
            Winner = winner,
            Sets = sets.ToArray()
        };
    }

    public void UpdateMatchDate(Guid matchId, DateOnly newDate)
    {
        var matchRecord = _database.GetMatchRecords().FirstOrDefault(m => m.Id == matchId);

        if (matchRecord is null)
        {
            return;
        }

        var updatedMatchRecord = matchRecord with { Date = newDate };

        _database.UpdateMatchRecord(updatedMatchRecord);
    }

    public void UpdateMatchWinner(Guid matchId, Guid winnerId)
    {
        var matchesInDatabase = _database.GetMatchRecords();

        var matchToUpdate = matchesInDatabase.FirstOrDefault(m => m.Id == matchId);

        if (matchToUpdate is null)
        {
            return;
        }

        if (matchToUpdate.FirstTeam != winnerId && matchToUpdate.SecondTeam != winnerId)
        {
            return;
        }

        matchToUpdate = matchToUpdate with { Winner = winnerId };

        _database.UpdateMatchRecord(matchToUpdate);

        var possibleParents =
            matchesInDatabase.Where(m => m.Id != matchToUpdate.Id && m.TournamentId == matchToUpdate.TournamentId);

        var firstParent = possibleParents.FirstOrDefault(m => m.FirstPreviousMach == matchToUpdate.Id || m.SecondPreviousMach == matchToUpdate.Id);

        if (firstParent is null)
        {
            return;
        }

        UpdateWinnerOfFirstParent(matchToUpdate, firstParent);

        var previousParent = firstParent;

        while (true)
        {
            var nextParent = possibleParents.FirstOrDefault(m => m.FirstPreviousMach == previousParent.Id || m.SecondPreviousMach == previousParent.Id);

            if (nextParent is null)
            {
                break;
            }

            ClearWinnerOfNextParent(previousParent, nextParent);

            previousParent = nextParent;
        }
    }

    public void DeleteMatchesByTournamentId(Guid tournamentId)
    {
        var matchRecords = _database.GetMatchRecords().Where(m => m.TournamentId == tournamentId);

        foreach (var matchRecord in matchRecords)
        {
            _setService.DeleteSetsForMatch(matchRecord.Id);
            _database.DeleteMatchRecord(matchRecord);
        }
    }

    private Guid CreateMatchRecursive(IReadOnlyList<Guid> availableTeamsIds, Guid tournamentId, ICollection<Guid> newMatchesIds)
    {
        MatchRecord newMatchRecord;

        if (availableTeamsIds.Count > 2)
        {
            var teamsAreEven = availableTeamsIds.Count % 2 == 0;
            var halfCount = availableTeamsIds.Count / 2;

            var firstHalfOfTeamIds = availableTeamsIds.Take(halfCount).ToArray();
            var secondHalfOfTeamIds = availableTeamsIds.TakeLast(halfCount + (teamsAreEven ? 0 : 1))
                .ToArray();

            Guid? firstTeamId = null;
            Guid? firstMatchId = null;

            if (firstHalfOfTeamIds.Length == 1)
            {
                firstTeamId = firstHalfOfTeamIds[0];
            }
            else
            {
                firstMatchId = CreateMatchRecursive(firstHalfOfTeamIds, tournamentId, newMatchesIds);
            }

            var secondMatchId = CreateMatchRecursive(secondHalfOfTeamIds, tournamentId, newMatchesIds);

            newMatchRecord = new MatchRecord(Guid.NewGuid(), tournamentId, null, firstMatchId, secondMatchId, firstTeamId, null, null);
        }
        else
        {
            var firstTeamId = availableTeamsIds[0];
            Guid? secondTeamId = null;

            if (availableTeamsIds.Count > 1)
            {
                secondTeamId = availableTeamsIds[1];
            }

            newMatchRecord = new MatchRecord(Guid.NewGuid(), tournamentId, null, null, null, firstTeamId, secondTeamId, null);
        }

        _database.UpdateMatchRecord(newMatchRecord);

        var newMatchId = newMatchRecord.Id;

        newMatchesIds.Add(newMatchId);

        return newMatchId;
    }

    private void GetTeamsAndWinner(MatchRecord matchRecord, out TeamModel? firstTeam, out TeamModel? secondTeam,
        out TeamModel? winner)
    {
        firstTeam = GetTeamModel(matchRecord.FirstTeam);
        secondTeam = GetTeamModel(matchRecord.SecondTeam);

        winner = null;
        UpdateWinner(ref winner, firstTeam, matchRecord.Winner);
        UpdateWinner(ref winner, secondTeam, matchRecord.Winner);

        return;

        TeamModel? GetTeamModel(Guid? teamId)
        {
            return teamId is not null ? _teamService.GetTeam(teamId.Value) : null;
        }

        void UpdateWinner(ref TeamModel? currentWinner, TeamModel? possibleWinner, Guid? winnerId)
        {
            if (possibleWinner is not null && possibleWinner.TeamId == winnerId)
            {
                currentWinner = possibleWinner;
            }
        }
    }

    private void UpdateWinnerOfFirstParent(MatchRecord child, MatchRecord firstParent)
    {
        var isFirstChild = firstParent.FirstPreviousMach == child.Id;

        if (child.Winner is null)
        {
            if (isFirstChild)
            {
                firstParent = firstParent with { FirstTeam = null, Winner = null };
            }
            else
            {
                firstParent = firstParent with { SecondTeam = null, Winner = null };
            }
        }
        else
        {
            if (isFirstChild)
            {
                firstParent = firstParent with { FirstTeam = child.Winner, Winner = null };
            }
            else
            {
                firstParent = firstParent with { SecondTeam = child.Winner, Winner = null };
            }
        }

        _database.UpdateMatchRecord(firstParent);
    }

    private void ClearWinnerOfNextParent(MatchRecord child, MatchRecord nextParent)
    {
        var isFirstChild = nextParent.FirstPreviousMach == child.Id;

        if (isFirstChild)
        {
            nextParent = nextParent with { FirstTeam = null, Winner = null };
        }
        else
        {
            nextParent = nextParent with { SecondTeam = null, Winner = null };
        }

        _database.UpdateMatchRecord(nextParent);
    }
}