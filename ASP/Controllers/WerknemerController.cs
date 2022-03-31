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

        [HttpPost]
        public string WerknemerToevoegen(string naam, int werknemerNum) 
        {
            Werknemer werknemer = new Werknemer(werknemerNum, naam, 123321);
            WerknemerContainer werknemerContainer = new WerknemerContainer();

            werknemerContainer.AddWerknemer(werknemer);
            return "goed";
        }
    }
}
