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
                    NummerPasje = werknemer.NummerPasje,
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
            return View();
        }

        [HttpPost]
        public void WerknemerToevoegen(string naam, int werknemerNum, int telefoonNum) 
        {
            Werknemer werknemer = new Werknemer(naam, werknemerNum, telefoonNum, new WerknemerDAL());
            werknemer.AddWerknemer();
        }

        [HttpPost]
        public void WerknemerAanpassen(string newNaam, int newWerknemerNum, int newTelefoonNum, int oldWerknemerID)
        {
            Werknemer werknemer = new Werknemer(newNaam, newWerknemerNum, newTelefoonNum, new WerknemerDAL());
            werknemer.UpdateWerknemer(oldWerknemerID);
        }

        [HttpPost]
        public void WerknemerVerwijderen(int id)
        {
            Werknemer werknemer = new Werknemer(new WerknemerDAL());
            werknemer.WerknemerID = id;
            werknemer.DeleteWerknemer();
        }

        [HttpPost]
        public ActionResult<Werknemer> LoadWerknemer(int id)
        {
            WerknemerContainer werknemerContainer = new WerknemerContainer(new WerknemerDAL());
            var output = werknemerContainer.GetWerknemer(id);

            if (output == null)
            {
                return Conflict("Geen werknemer gevonden");
            }

            return Ok(output);
        }
    }
}
