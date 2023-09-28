using TournamentManagementLogic.Model;

namespace TournamentManagementBlazor.Model.Tournament;

public static class TournamentExtensions
{
    public static string ConvertGameName(this TournamentModel tournamentModel) => !string.IsNullOrWhiteSpace(tournamentModel.GameName) ? tournamentModel.GameName : "Undefined game";

    public static string ConvertTeamName(this TeamModel? team) => team is not null ? team.TeamName : "-";

    public static string ConvertWinnerName(this TeamModel? winner) => winner is null ? "The winner of the match has not been selected." : $"Winner {winner.TeamName}";

    public static string ConvertDate(this TournamentMatchModel matchModel)
    {
        var date = matchModel.Date;

        if (!date.HasValue)
        {
            return "The match date has not been selected.";
        }

        var dateValue = date.Value;

        return $"Match date {dateValue}";
    }

    public static IEnumerable<TournamentRoundModel> ConvertRounds(this TournamentModel tournamentModel)
    {
        var currentStaticNameIndex = 0;
        var roundIndex = 0;

        return tournamentModel.Rounds.Select(r =>
        {
            var round = new TournamentRoundModel
            {
                RoundName = GetNextRoundName(),
                Matches = ConvertMatches(r)
            };

            roundIndex++;

            return round;

        }).Reverse();

        string GetNextRoundName()
        {
            var staticRoundsNames = new[] { "Finals", "Semi Finals", "Quarter Finals" };
            const string dynamicRoundName = "Round {0}";
            string name;

            if (currentStaticNameIndex < staticRoundsNames.Length)
            {
                name = staticRoundsNames[currentStaticNameIndex];
                currentStaticNameIndex++;
            }
            else
            {
                var roundNameIndex = tournamentModel.Rounds.Count - roundIndex;

                name = string.Format(dynamicRoundName, roundNameIndex);
            }

            return name;
        }
    }

    private static ICollection<TournamentMatchModel> ConvertMatches(IEnumerable<MatchModel> matches)
    {
        return matches.Select(match => new TournamentMatchModel
        {
            MatchId = match.MatchId,
            Date = match.Date,
            FirstTeam = match.FirstTeam,
            SecondTeam = match.SecondTeam,
            Winner = match.Winner,
            Sets = ConvertSets(match.Sets)
        }).ToList();
    }

    private static IList<TournamentSetModel> ConvertSets(IEnumerable<SetModel> sets)
    {
        return sets.Select(set => new TournamentSetModel
        {
            SetId = set.SetId,
            FirstTeamScore = set.FirstTeamScore,
            SecondTeamScore = set.SecondTeamScore
        }).ToList();
    }
}
