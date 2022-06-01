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

        private List<WerknemerViewModel> GetUserWerknemers()
        {
            WerknemerContainer werknemerContainer = new WerknemerContainer(new WerknemerDAL());
            List<WerknemerViewModel> werknemerViewModels = new List<WerknemerViewModel>();
            foreach (Werknemer werknemer in werknemerContainer.GetUserWerknemers(HttpContext.Session.GetInt32("user-id").Value))
            {
                WerknemerViewModel werknemerViewModel = new WerknemerViewModel
                {
                    WerknemerID = werknemer.WerknemerID,
                    Naam = werknemer.Naam,
                    WerknemerNummer = werknemer.WerknemerNummer,
                    TelefoonNummer = werknemer.TelefoonNummer
                };
                werknemerViewModels.Add(werknemerViewModel);
            }
            return werknemerViewModels;
        }

        private List<WerknemerViewModel> GetAllWerknemers()
        {
            WerknemerContainer werknemerContainer = new WerknemerContainer(new WerknemerDAL());
            List<WerknemerViewModel> werknemerViewModels = new List<WerknemerViewModel>();
            foreach (Werknemer werknemer in werknemerContainer.GetAllWerknemers())
            {
                WerknemerViewModel werknemerViewModel = new WerknemerViewModel
                {
                    WerknemerID = werknemer.WerknemerID,
                    Naam = werknemer.Naam,
                    WerknemerNummer = werknemer.WerknemerNummer,
                    TelefoonNummer = werknemer.TelefoonNummer
                };
                werknemerViewModels.Add(werknemerViewModel);
            }
            return werknemerViewModels;
        }

        [HttpGet]
        public IActionResult WerknemerToevoegenView()
        {
            ViewData["Werknemers"] = GetAllWerknemers();
            return View();
        }

        [HttpGet]
        public IActionResult WerknemerAanpassenView()
        {
            return View(GetUserWerknemers());
        }

        [HttpGet]
        public IActionResult WerknemerVerwijderenView()
        {
            return View(GetUserWerknemers());
        }

        [HttpPost]
        public ActionResult WerknemerToevoegen(WerknemerViewModel werknemerViewModel) 
        {
            if (ModelState.IsValid)
            {
                Werknemer werknemer = new Werknemer(werknemerViewModel.Naam, werknemerViewModel.WerknemerNummer, werknemerViewModel.TelefoonNummer, new WerknemerDAL());
                werknemer.AddWerknemer(HttpContext.Session.GetInt32("user-id").Value);
                return RedirectToAction("Index", "Home");
            }
            else
                return BadRequest();
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

        [HttpPost]
        public ActionResult LinkWerknemerToUser(int ID)
        {
            Werknemer werknemer = new Werknemer(new WerknemerDAL())
            {
                WerknemerID = ID
            };

            if (werknemer.LinkWerknemerToUser(HttpContext.Session.GetInt32("user-id").Value) == 1)
            {
                return Ok("Succesvol Gelinkt");
            }
            return BadRequest("Fout");
        }
    }
}
