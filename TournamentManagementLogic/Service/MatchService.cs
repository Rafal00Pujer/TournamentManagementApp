using TournamentManagementLogic.Database;
using TournamentManagementLogic.Model;
using TournamentManagementLogic.Service.Interfaces;

namespace TournamentManagementLogic.Service;

public class MatchService : IMatchService
{
    private readonly IDatabase _database;

    public MatchService(IDatabase database)
    {
        _database = database;
    }

    public Guid CreateMatch(Guid tournamentId, Guid? firstPreviousMatch, Guid? secondPreviousMatch, Guid? firstTeam, Guid? secondTeam)
    {
        var newMatchRecord = new MatchRecord(Guid.NewGuid(), tournamentId, null, firstPreviousMatch, secondPreviousMatch, firstTeam, secondTeam, null);

        _database.UpdateMatchRecord(newMatchRecord);

        return newMatchRecord.Id;
    }

    public List<MatchModel> GetMatchesForTournament(Guid tournamentId)
    {
        throw new NotImplementedException();

        //var matchesInDatabase = _database.GetMatchRecords();

        //var matchesInTournament = matchesInDatabase.Where(m => m.TournamentId == tournamentId);

        //var matches = new List<MatchModel>();

        //foreach (var matchRecord in matchesInTournament)
        //{
        //    CreateMatch(matchRecord);
        //}

        //return matches;

        //// ReSharper disable once LocalFunctionHidesMethod
        //MatchModel CreateMatch(MatchRecord matchRecord)
        //{
        //    MatchModel? firstPreviousMatch = null;
        //    MatchModel? secondPreviousMatch = null;

        //    if (matchRecord.FirstPreviousMach is not null)
        //    {
        //        firstPreviousMatch = matches.FirstOrDefault(m => m.Id == matchRecord.FirstPreviousMach);

        //        if (firstPreviousMatch is null)
        //        {
        //            var firstPreviousMatchRecord = matchesInTournament.First(m => m.Id == matchRecord.FirstPreviousMach);

        //            firstPreviousMatch = CreateMatch(firstPreviousMatchRecord);
        //        }
        //    }

        //    if (matchRecord.SecondPreviousMach is not null)
        //    {
        //        secondPreviousMatch = matches.FirstOrDefault(m => m.Id == matchRecord.SecondPreviousMach);

        //        if (secondPreviousMatch is null)
        //        {
        //            var secondPreviousMatchRecord = matchesInTournament.First(m => m.Id == matchRecord.SecondPreviousMach);

        //            secondPreviousMatch = CreateMatch(secondPreviousMatchRecord);
        //        }
        //    }

        //    var newMatch = new MatchModel()
        //    {
        //        Id = matchRecord.Id,
        //        Date = matchRecord.Date,
        //        FirstTeam = matchRecord.FirstTeam,
        //        SecondTeam = matchRecord.SecondTeam,
        //        Winner = matchRecord.Winner,

        //        FirstPreviousMatch = firstPreviousMatch,
        //        SecondPreviousMatch = secondPreviousMatch
        //    };

        //    matches.Add(newMatch);

        //    return newMatch;
        //}
    }

    public void DeleteMatchesForTournament(Guid tournamentId)
    {
        var matchesInDatabase = _database.GetMatchRecords();

        var matchesInTournament = matchesInDatabase.Where(m => m.TournamentId == tournamentId);

        foreach (var matchRecord in matchesInTournament)
        {
            _database.DeleteMatchRecord(matchRecord);
        }
    }

    public MatchBasicModel GetMatchById(Guid matchId)
    {
        var matchRecord = _database.GetMatchRecords().FirstOrDefault(m => m.Id == matchId);

        if (matchRecord is null)
        {
            return new MatchBasicModel();
        }

        TeamModel? firstTeam = null;
        TeamModel? secondTeam = null;
        TeamModel? winner = null;

        if (matchRecord.FirstTeam is not null)
        {
            var firstTeamRecord = _database.GetTeamRecords().FirstOrDefault(t => t.Id == matchRecord.FirstTeam);

            if (firstTeamRecord is not null)
            {
                firstTeam = new TeamModel
                {
                    Id = firstTeamRecord.Id,
                    Name = firstTeamRecord.Name,
                };

                if (firstTeamRecord.Id == matchRecord.Winner)
                {
                    winner = firstTeam;
                }
            }
        }

        if (matchRecord.SecondTeam is not null)
        {
            var secondTeamRecord = _database.GetTeamRecords().FirstOrDefault(t => t.Id == matchRecord.SecondTeam);

            if (secondTeamRecord is not null)
            {
                secondTeam = new TeamModel
                {
                    Id = secondTeamRecord.Id,
                    Name = secondTeamRecord.Name,
                };

                if (secondTeamRecord.Id == matchRecord.Winner)
                {
                    winner = secondTeam;
                }
            }
        }

        var sets = _database.GetSetRecords()
            .Where(s => s.MatchId == matchRecord.Id)
            .Select(s => new SetModel
            {
                Id = s.Id,
                SetNumber = s.SetNumber,
                FirstTeamScore = s.FirstTeamScore,
                SecondTeamScore = s.SecondTeamScore
            }).ToList();

        sets.Sort((s1, s2) =>
        {
            if (s1.SetNumber < s2.SetNumber)
            {
                return -1;
            }

            if (s1.SetNumber > s2.SetNumber)
            {
                return 1;
            }

            return 0;
        });

        return new MatchBasicModel
        {
            Id = matchRecord.Id,
            Date = matchRecord.Date,
            FirstTeam = firstTeam,
            SecondTeam = secondTeam,
            Winner = winner,
            Sets = sets
        };
    }

    public void UpdateMatchDate(MatchBasicModel match)
    {
        var matchRecord = _database.GetMatchRecords().FirstOrDefault(m => m.Id == match.Id);

        if (matchRecord is null)
        {
            return;
        }

        var updatedMatchRecord = matchRecord with { Date = match.Date };

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

        UpdateFirstParent(matchToUpdate, firstParent);

        var previousParent = firstParent;

        while (true)
        {
            var nextParent = possibleParents.FirstOrDefault(m => m.FirstPreviousMach == previousParent.Id || m.SecondPreviousMach == previousParent.Id);

            if (nextParent is null)
            {
                break;
            }

            UpdateNextParent(previousParent, nextParent);

            previousParent = nextParent;
        }
    }

    private void UpdateFirstParent(MatchRecord child, MatchRecord firstParent)
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

    private void UpdateNextParent(MatchRecord child, MatchRecord nextParent)
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
