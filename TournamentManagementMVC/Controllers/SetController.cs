using Microsoft.AspNetCore.Mvc;
using TournamentManagementLogic.Service.Interfaces;
using TournamentManagementMVC.Models.Match;

namespace TournamentManagementMVC.Controllers
{
    public class SetController : Controller
    {
        private readonly ISetService _setService;

        public SetController(ISetService setService)
        {
            _setService = setService;
        }

        public IActionResult Save(MatchEditSetModel model, Guid matchId)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Edit", "Match", new { matchId });
            }

            if (model.SetId == Guid.Empty)
            {
                var newSetId = _setService.CreateEmptySet(matchId);

                model.SetId = newSetId;
            }

            _setService.UpdateScores(model.SetId, model.FirstTeamScore, model.SecondTeamScore);

            return RedirectToAction("Edit", "Match", new { matchId });
        }

        public IActionResult Delete(Guid setId, Guid matchId)
        {
            _setService.DeleteSet(setId);

            return RedirectToAction("Edit", "Match", new { matchId });
        }
    }
}
