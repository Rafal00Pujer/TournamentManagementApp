using Microsoft.AspNetCore.Mvc;
using TournamentManagementLogic.Model;
using TournamentManagementLogic.Service.Interfaces;

namespace TournamentManagementMVC.Controllers
{
    public class TournamentController : Controller
    {
        private readonly ITournamentService _service;

        public TournamentController(ITournamentService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            return View(_service.GetTournamentList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateTournamentModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            _service.CreateTournament(model);

            return RedirectToAction("Index");
        }

        public IActionResult Delete(Guid id)
        {
            return View(_service.GetTournamentBasicById(id));
        }

        [HttpPost]
        public IActionResult ConfirmDelete(Guid id)
        {
            _service.DeleteTournament(id);

            return RedirectToAction("Index");
        }

        public IActionResult Open(Guid id)
        {
            return View(_service.GetTournamentById(id));
        }
    }
}
