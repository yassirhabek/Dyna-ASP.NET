using Microsoft.AspNetCore.Mvc;

namespace ASP.Controllers
{
    public class RouteController : Controller
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
        public IActionResult RouteToevoegenView()
        {
            return View();
        }
    }
}
