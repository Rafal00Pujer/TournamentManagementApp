using Microsoft.AspNetCore.Mvc;
using TournamentManagementLogic.Model;
using TournamentManagementLogic.Service.Interfaces;
using TournamentManagementMVC.Models.Match;

namespace TournamentManagementMVC.Controllers
{
    public class MatchController : Controller
    {
        private readonly IMatchService _matchService;

        public MatchController(IMatchService matchService)
        {
            _matchService = matchService;
        }

        public IActionResult Edit(Guid matchId)
        {
            var match = _matchService.GetMatchById(matchId);

            var model = new MatchEditModel
            {
                TournamentId = match.TournamentId,
                MatchId = match.MatchId,
                Date = match.Date
            };

            ConvertTeamModel(match.FirstTeam, model.FirstTeam, match.Winner);
            ConvertTeamModel(match.SecondTeam, model.SecondTeam, match.Winner);

            foreach (var set in match.Sets)
            {
                var setModel = new MatchEditSetModel
                {
                    MatchId = set.MatchId,
                    SetId = set.SetId,
                    FirstTeamScore = set.FirstTeamScore,
                    SecondTeamScore = set.SecondTeamScore
                };

                model.Sets.Add(setModel);
            }

            return View(model);
        }

        public IActionResult SetWinner(Guid matchId, Guid winnerId)
        {
            _matchService.UpdateMatchWinner(matchId, winnerId);

            return RedirectToAction("Edit", new { matchId });
        }

        public IActionResult SetDate(MatchEditModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Edit", new { matchId = model.MatchId });
            }

            _matchService.UpdateMatchDate(model.MatchId, model.Date ?? DateOnly.FromDateTime(DateTime.Now));

            return RedirectToAction("Edit", new { matchId = model.MatchId });
        }

        private static void ConvertTeamModel(TeamModel? fromTeamModel, MatchEditTeamModel toTeamModel, TeamModel? winner)
        {
            if (fromTeamModel is null)
            {
                return;
            }

            toTeamModel.TeamId = fromTeamModel.TeamId;
            toTeamModel.TeamName = fromTeamModel.TeamName;

            if (fromTeamModel == winner)
            {
                toTeamModel.IsWinner = true;
            }
        }
    }
}