namespace TournamentManagementBlazor.Model.Tournament;

public class TournamentRoundModel
    {
        public string RoundName { get; init; } = string.Empty;

        public ICollection<TournamentMatchModel> Matches { get; init; } = new List<TournamentMatchModel>();
    }

