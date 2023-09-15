using TournamentManagementLogic.Database;
using TournamentManagementLogic.Model;
using TournamentManagementLogic.Service.Interfaces;

namespace TournamentManagementLogic.Service;

public class TournamentRoundsService : ITournamentRoundsService
{
    private readonly IDatabase _database;
    private readonly IMatchService _matchService;

    public TournamentRoundsService(IDatabase database, IMatchService matchService)
    {
        _database = database;
        _matchService = matchService;
    }

    public IReadOnlyCollection<IReadOnlyCollection<MatchModel>> GetTournamentRounds(Guid tournamentId)
    {
        var tournamentMatches = _database.GetMatchRecords().Where(m => m.TournamentId == tournamentId).ToArray();

        var finalMatchRecord = GetFinalMatch();

        var rounds = new List<List<MatchModel>>();

        var nextRoundQueue = new Queue<MatchRecord>();
        nextRoundQueue.Enqueue(finalMatchRecord);

        while (nextRoundQueue.Count > 0)
        {
            var currentRound = new List<MatchModel>();
            rounds.Add(currentRound);

            var currentRoundQueue = nextRoundQueue;
            nextRoundQueue = new Queue<MatchRecord>();

            while (currentRoundQueue.Count > 0)
            {
                var matchRecord = currentRoundQueue.Dequeue();

                EnqueuePreviousMatch(matchRecord.FirstPreviousMach);
                EnqueuePreviousMatch(matchRecord.SecondPreviousMach);

                currentRound.Add(_matchService.GetMatchById(matchRecord.Id));

                continue;

                void EnqueuePreviousMatch(Guid? previousMatchId)
                {
                    if (previousMatchId is not null)
                    {
                        nextRoundQueue.Enqueue(tournamentMatches.First(m => m.Id == previousMatchId));
                    }
                }
            }
        }

        return rounds;

        MatchRecord GetFinalMatch() => tournamentMatches.Length switch
        {
            1 => tournamentMatches[0],
            2 => tournamentMatches.First(m => m.SecondPreviousMach is not null),
            _ => tournamentMatches.First(m1 =>
                m1.FirstPreviousMach is not null && m1.SecondPreviousMach is not null && tournamentMatches
                    .Where(m2 => m2 != m1)
                    .All(m2 => m2.FirstPreviousMach != m1.Id && m2.SecondPreviousMach != m1.Id))
        };
    }
}

