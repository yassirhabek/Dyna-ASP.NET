using Logic.Containers;
using Logic.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASP.Controllers
{
    public class WerknemerController : Controller
    {
        public string Index()
        {
            return "123321";
        }
        
        public string Message()
        {
            return "this is a message";
        }

        public IActionResult Page()
        {
            ViewData["pageMessage"] = "this is a message";
            return View();
        }

        [HttpGet]
        public IActionResult Page(string msg)
        {
            ViewData["pageMessage"] = string.IsNullOrEmpty(msg) ? "no message provided" : msg;
            return View();
        }

        [HttpGet]
        public IActionResult WerknemerToevoegenView()
        {
            return View();
        }

        [HttpGet]
        public IActionResult WerknemerAanpassenView()
        {
            return View();
        }

        [HttpGet]
        public IActionResult WerknemerVerwijderenView()
        {
            return View();
        }

        [HttpPost]
        public void WerknemerToevoegen(string naam, int werknemerNum, int telefoonNum) 
        {
            WerknemerContainer werknemerContainer = new WerknemerContainer();
            werknemerContainer.AddWerknemer(naam, werknemerNum, telefoonNum);
        }

        [HttpPost]
        public void WerknemerAanpassen(string newNaam, int newWerknemerNum, int newTelefoonNum, int oldWerknemerID)
        {
            WerknemerContainer werknemerContainer = new WerknemerContainer();
            werknemerContainer.UpdateWerknemer(newNaam, newWerknemerNum, newTelefoonNum, oldWerknemerID);
        }

        [HttpPost]
        public void WerknemerVerwijderen(int id)
        {
            WerknemerContainer werknemerContainer = new WerknemerContainer();
            werknemerContainer.DeleteWerknemer(id);
        }

        [HttpPost]
        public ActionResult<Werknemer> LoadWerknemer(int id)
        {
            WerknemerContainer werknemerContainer = new WerknemerContainer();
            var output = werknemerContainer.GetWerknemer(id);

            if (output == null)
            {
                return Conflict("Geen werknemer gevonden");
            }

            return Ok(output);
        }
    }
}
