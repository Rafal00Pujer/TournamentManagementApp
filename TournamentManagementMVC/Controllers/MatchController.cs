using Microsoft.AspNetCore.Mvc;
using TournamentManagementLogic.Model;
using TournamentManagementLogic.Service.Interfaces;

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
            return View(_matchService.GetMatchById(matchId));
        }

        public IActionResult SetWinner(Guid matchId, Guid winnerId)
        {
            _matchService.UpdateMatchWinner(matchId, winnerId);

            return RedirectToAction("Edit", new { matchId });
        }

        public IActionResult SetDate(MatchBasicModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Edit", new { matchId = model.Id });
            }

            _matchService.UpdateMatchDate(model);

            return RedirectToAction("Edit", new { matchId = model.Id });
        }
    }
}