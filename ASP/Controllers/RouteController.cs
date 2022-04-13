using ASP.Models;
using DAL.DAL;
using Logic.Containers;
using Logic.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASP.Controllers
{
    public class RouteController : Controller
    {
        private readonly ILogger<RouteController> _logger;

        public RouteController(ILogger<RouteController> logger)
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

        private List<RouteViewModel> GetRoutes()
        {
            RouteContainer routeContainer = new RouteContainer(new RouteDAL());
            List<RouteViewModel> routeViewModels = new List<RouteViewModel>();
            foreach (var routes in routeContainer.GetRoute(new WerknemerDAL(), new WerknemerDAL()))
            {
                RouteViewModel routeViewModel = new RouteViewModel
                {
                    RouteID = routes.RouteID,
                    RouteNummer = routes.RouteNummer,
                    Datum = routes.Datum,
                    Chauffeur = routes.Chauffeur,
                    BijRijder = routes.BijRijder,
                    StartTijd = routes.StartTijd,
                    EindTijd = routes.EindTijd,
                    AantalUur = routes.AantalUur,
                    Bijzonderheden = routes.Bijzonderheden
                };
                routeViewModels.Add(routeViewModel);
            }
            return routeViewModels;
        }

        [HttpGet]
        public IActionResult RouteToevoegenView()
        {
            ViewData["Routes"] = GetRoutes();
            ViewData["Werknemers"] = GetWerknemers();
            return View();
        }

        [HttpPost]
        public ActionResult RouteToevoegen(int routeNummer, string datum, Werknemer chauffeur, Werknemer bijrijder, string startTijd, string eindTijd)
        {
            if (ModelState.IsValid)
            {
                DateTime pardedDatum;
                TimeSpan parsedStartTijd;
                TimeSpan parsedEindTijd;

                WerknemerContainer werknemerContainer = new WerknemerContainer(new WerknemerDAL());
                //Werknemer chauffeur = werknemerContainer.GetWerknemers().FirstOrDefault(w => w.WerknemerID == chauffeurID);
                //Werknemer bijrijder = werknemerContainer.GetWerknemers().FirstOrDefault(w => w.WerknemerID == bijrijderID);


                DateTime.TryParse(datum, out pardedDatum);
                TimeSpan.TryParse(startTijd, out parsedStartTijd);
                TimeSpan.TryParse(eindTijd, out parsedEindTijd);

                RouteRit newRoute = new RouteRit(routeNummer, pardedDatum, chauffeur, bijrijder, parsedStartTijd, parsedEindTijd, new RouteDAL());
                newRoute.AddRoute();
                return RedirectToAction("Index", "Home");
            }
            return View(RouteToevoegenView());
        }

        [HttpGet]
        public IActionResult RouteVerwijderenView()
        {
            return View(GetRoutes());
        }
    }
}
