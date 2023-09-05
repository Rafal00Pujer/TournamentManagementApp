using Microsoft.AspNetCore.Mvc;
using TournamentManagementLogic.Model;
using TournamentManagementLogic.Service;
using TournamentManagementLogic.Service.Interfaces;

namespace TournamentManagementMVC.Controllers
{
    public class SetController : Controller
    {
        private readonly ISetService _setService;

        public SetController(ISetService setService)
        {
            _setService = setService;
        }

        public IActionResult Save(SetModel model, Guid matchId)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Edit", "Match", new { matchId });
            }

            if (model.Id == Guid.Empty || model.SetNumber == 0)
            {
                var newSetId = _setService.CreateEmptySet(matchId);

                var newSetModel = _setService.GetSet(newSetId);

                newSetModel.FirstTeamScore = model.FirstTeamScore;
                newSetModel.SecondTeamScore = model.SecondTeamScore;

                model = newSetModel;
            }

            _setService.UpdateScores(model);

            return RedirectToAction("Edit", "Match", new { matchId });
        }

        public IActionResult Delete(Guid setId, Guid matchId)
        {
            _setService.DeleteSet(setId);

            return RedirectToAction("Edit", "Match", new { matchId });
        }
    }
}
