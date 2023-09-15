using Microsoft.AspNetCore.Mvc;
using TournamentManagementLogic.Model;
using TournamentManagementLogic.Service.Interfaces;
using TournamentManagementMVC.Models.Tournament;
using TournamentManagementMVC.Models.Tournament.Create;
using TournamentManagementMVC.Models.Tournament.Open;

namespace TournamentManagementMVC.Controllers
{
    public class TournamentController : Controller
    {
        private static readonly string[] StaticRoundsNames = new[] { "Finals", "Semi Finals", "Quarter Finals" };
        private const string DynamicRoundName = "Round {0}";

        private readonly ITournamentService _service;

        public TournamentController(ITournamentService service)
        {
            _service = service;
        }

        public IActionResult List()
        {
            var tournaments = _service.GetTournaments();

            var model = tournaments.Select(t =>
                new TournamentListElementModel
                {
                    TournamentId = t.TournamentId,
                    TournamentName = t.TournamentName,
                    GameName = t.GameName,
                    TournamentDescription = t.TournamentDescription
                });

            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(TournamentCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var teamsNames = model.Teams.Select(t => t.Name);

            _ = _service.CreateTournament(model.TournamentName, model.GameName, model.TournamentDescription, teamsNames);

            return RedirectToAction("List");
        }
        public IActionResult Delete(Guid tournamentId)
        {
            _service.DeleteTournament(tournamentId);

            return RedirectToAction("List");
        }

        public IActionResult Open(Guid tournamentId)
        {
            var tournament = _service.GetTournamentById(tournamentId);

            if (tournament.TournamentId == Guid.Empty)
            {
                return RedirectToAction("List");
            }

            var rounds = ConvertRoundsModels(tournament.Rounds);

            var model = new TournamentOpenModel
            {
                TournamentId = tournament.TournamentId,
                TournamentName = tournament.TournamentName,
                GameName = tournament.GameName,
                TournamentDescription = tournament.TournamentDescription,
                Rounds = rounds
            };

            return View(model);
        }

        private static IReadOnlyCollection<TournamentOpenRoundModel> ConvertRoundsModels(IReadOnlyCollection<IReadOnlyCollection<MatchModel>> rounds)
        {
            var currentStaticNameIndex = 0;
            var roundIndex = 0;
            var roundsModels = new List<TournamentOpenRoundModel>();

            foreach (var round in rounds)
            {
                var roundModel = new TournamentOpenRoundModel
                {
                    RoundName = GetRoundName(),
                    RoundMatches = ConvertRoundMatchesModels(round)
                };

                roundsModels.Add(roundModel);

                roundIndex++;
            }

            roundsModels.Reverse();

            return roundsModels;

            string GetRoundName()
            {
                string name;

                if (currentStaticNameIndex < StaticRoundsNames.Length)
                {
                    name = StaticRoundsNames[currentStaticNameIndex];
                    currentStaticNameIndex++;
                }
                else
                {
                    var roundNameIndex = rounds.Count - roundIndex;

                    name = string.Format(DynamicRoundName, roundNameIndex);
                }

                return name;
            }
        }

        private static IReadOnlyCollection<TournamentOpenMatchModel> ConvertRoundMatchesModels(IEnumerable<MatchModel> roundMatches)
        {
            var matchesModels = new List<TournamentOpenMatchModel>();

            foreach (var roundMatch in roundMatches)
            {
                var matchModel = new TournamentOpenMatchModel
                {
                    MatchId = roundMatch.MatchId,
                    MatchDate = roundMatch.Date,
                };

                if (roundMatch.FirstTeam is not null)
                {
                    matchModel.MatchTeams[0] = CreateTeamOpenModel(roundMatch.FirstTeam,
                        roundMatch.FirstTeam == roundMatch.Winner, roundMatch.Sets.Select(s => s.FirstTeamScore).ToArray());
                }
                else
                {
                    matchModel.MatchTeams[0] = new TournamentOpenTeamModel();
                }

                if (roundMatch.SecondTeam is not null)
                {
                    matchModel.MatchTeams[1] = CreateTeamOpenModel(roundMatch.SecondTeam,
                        roundMatch.SecondTeam == roundMatch.Winner, roundMatch.Sets.Select(s => s.SecondTeamScore).ToArray());
                }
                else
                {
                    matchModel.MatchTeams[1] = new TournamentOpenTeamModel();
                }

                matchesModels.Add(matchModel);
            }

            return matchesModels;
        }

        private static TournamentOpenTeamModel CreateTeamOpenModel(TeamModel team, bool isWinner, IReadOnlyList<string> teamSetsScores)
        {
            var teamOpenModel = new TournamentOpenTeamModel
            {
                TeamName = team.TeamName,
                IsWinner = isWinner
            };

            if (teamSetsScores.Count > 3)
            {
                teamOpenModel.TeamSetsScores[0] = teamSetsScores[0];
                teamOpenModel.TeamSetsScores[1] = "...";
                teamOpenModel.TeamSetsScores[2] = teamSetsScores[2];
            }
            else
            {
                for (var i = 0; i < 3 && i < teamSetsScores.Count; i++)
                {
                    teamOpenModel.TeamSetsScores[i] = teamSetsScores[i];
                }
            }

            return teamOpenModel;
        }
    }
}
