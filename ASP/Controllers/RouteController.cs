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
                    NummerPasje = werknemer.WerknemerNummer,
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
            foreach (var routes in routeContainer.GetRoutes(new WerknemerDAL(), new WerknemerDAL()))
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

        [HttpPost]
        public ActionResult GetRoutesFromDate(string date)
        {
            RouteContainer routeContainer = new RouteContainer(new RouteDAL());
            List<RouteViewModel> routeViewModels = new List<RouteViewModel>();
            foreach (var routes in routeContainer.GetRoutesFromDate(date, new WerknemerDAL(), new WerknemerDAL()))
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
            return Ok(routeViewModels);
        }

        [HttpGet]
        public IActionResult RouteToevoegenView()
        {
            ViewData["Routes"] = GetRoutes();
            ViewData["Werknemers"] = GetWerknemers();
            return View();
        }

        [HttpPost]
        public ActionResult RouteToevoegen(int routeNummer, string rawDatum, int chauffeurID, int bijrijderID, string rawStartTijd, string rawEindTijd)
        {
            DateTime parsedDatum;
            TimeSpan parsedStartTijd;
            TimeSpan parsedEindTijd;

            WerknemerContainer werknemerContainer = new WerknemerContainer(new WerknemerDAL());
            Werknemer chauffeur = werknemerContainer.GetWerknemers().FirstOrDefault(w => w.WerknemerID == chauffeurID);
            Werknemer bijrijder = werknemerContainer.GetWerknemers().FirstOrDefault(w => w.WerknemerID == bijrijderID);


            DateTime.TryParse(rawDatum, out parsedDatum);
            TimeSpan.TryParse(rawStartTijd, out parsedStartTijd);
            TimeSpan.TryParse(rawEindTijd, out parsedEindTijd);

            RouteRit newRoute = new RouteRit(routeNummer, parsedDatum, chauffeur, bijrijder, parsedStartTijd, parsedEindTijd, new RouteDAL());
            newRoute.AddRoute();
            return Ok("Route Toegevoegd");
        }

        [HttpGet]
        public IActionResult RouteAanpassenView()
        {
            return View();
        }

        [HttpPost]
        public ViewResult RouteAanpassenForm(int id)
        {
            var viewData = new RouteViewModel()
            {
                RouteID = id
            };

            return View(viewData);
        }

        [HttpGet]
        public IActionResult RouteVerwijderenView()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RouteVerwijderen(int id)
        {
            RouteContainer routeContainer = new RouteContainer(new RouteDAL());
            RouteRit route = routeContainer.GetRoutes(new WerknemerDAL(), new WerknemerDAL()).FirstOrDefault(r => r.RouteID == id);

            route.DeleteRoute();
            return Ok("Route Verwijderd");
        }


    }
}
