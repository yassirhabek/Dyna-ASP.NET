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
        public void WerknemerToevoegen(string naam, int werknemerNum, int telefoonNum) 
        {
            WerknemerContainer werknemerContainer = new WerknemerContainer();
            werknemerContainer.AddWerknemer(naam, werknemerNum, telefoonNum);
        }

        [HttpGet]
        public IActionResult WerknemerVerwijderenView()
        {
            return View();
        }

        [HttpPost]
        public void WerknemerVerwijderen(int werknemerID)
        {
            WerknemerContainer werknemerContainer = new WerknemerContainer();
            werknemerContainer.DeleteWerknemer(werknemerID);
        }

    }
}
