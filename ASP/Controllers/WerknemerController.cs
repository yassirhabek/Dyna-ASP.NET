using ASP.Models;
using DAL.DAL;
using Logic.Containers;
using Logic.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASP.Controllers
{
    public class WerknemerController : Controller
    {
        private readonly ILogger<WerknemerController> _logger;

        public WerknemerController(ILogger<WerknemerController> logger)
        {
            _logger = logger;
        }

        private List<WerknemerViewModel> GetWerknemers()
        {
            WerknemerContainer werknemerContainer = new WerknemerContainer(new WerknemerDAL());
            List<WerknemerViewModel> werknemerViewModels = new List<WerknemerViewModel>();
            foreach (var werknemer in werknemerContainer.GetWerknemers())
            {
                WerknemerViewModel werknemerViewModel = new WerknemerViewModel
                {
                    WerknemerID = werknemer.WerknemerID,
                    Naam = werknemer.Naam,
                    NummerPasje = werknemer.WerknemerNummer,
                    TelefoonNummer = werknemer.TelefoonNummer
                };
                werknemerViewModels.Add(werknemerViewModel);
            }
            return werknemerViewModels;
        }

        [HttpGet]
        public IActionResult WerknemerToevoegenView()
        {
            return View();
        }

        [HttpGet]
        public IActionResult WerknemerAanpassenView()
        {
            return View(GetWerknemers());
        }

        [HttpGet]
        public IActionResult WerknemerVerwijderenView()
        {
            return View(GetWerknemers());
        }

        [HttpPost]
        public ActionResult WerknemerToevoegen(string naam, int werknemerNum, int telefoonNum) 
        {
            if (ModelState.IsValid)
            {
                Werknemer werknemer = new Werknemer(naam, werknemerNum, telefoonNum, new WerknemerDAL());
                werknemer.AddWerknemer();
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public ActionResult WerknemerAanpassen(string newNaam, int newWerknemerNum, int newTelefoonNum, int oldWerknemerID)
        {
            Werknemer werknemer = new Werknemer(newNaam, newWerknemerNum, newTelefoonNum, new WerknemerDAL());
            werknemer.UpdateWerknemer(oldWerknemerID);
            return Ok("Werknemer aangepast");
        }

        [HttpPost]
        public ActionResult WerknemerVerwijderen(int id)
        {
            Werknemer werknemer = new Werknemer(new WerknemerDAL());
            werknemer.WerknemerID = id;
            werknemer.DeleteWerknemer();
            return Ok("Werknemer verwijderd");
        }

        [HttpPost]
        public ActionResult<Werknemer> LoadWerknemer(int id)
        {
            WerknemerContainer werknemerContainer = new WerknemerContainer(new WerknemerDAL());
            Werknemer output = werknemerContainer.GetSingleWerknemer(id);

            if (output == null)
            {
                return Conflict("Geen werknemer gevonden");
            }

            return Ok(output);
        }
    }
}
