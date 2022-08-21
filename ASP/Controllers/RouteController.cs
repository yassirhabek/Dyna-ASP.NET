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
            foreach (var werknemer in werknemerContainer.GetUserWerknemers(HttpContext.Session.GetInt32("user-id").Value))
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

        private List<RouteViewModel> GetRoutes()
        {
            RouteContainer routeContainer = new RouteContainer(new RouteDAL());
            List<RouteViewModel> routeViewModels = new List<RouteViewModel>();
            foreach (var routes in routeContainer.GetRoutes(new WerknemerDAL(), new WerknemerDAL(), HttpContext.Session.GetInt32("user-id").Value))
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
            foreach (var routes in routeContainer.GetRoutesFromDate(date, new WerknemerDAL(), new WerknemerDAL(), HttpContext.Session.GetInt32("user-id").Value))
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
        public ActionResult RouteToevoegen(int routeNummer, string rawDatum, int chauffeurID, int bijrijderID, string rawStartTijd, string rawEindTijd, string bijzonderheden)
        {
            DateTime parsedDatum;
            TimeSpan parsedStartTijd;
            TimeSpan parsedEindTijd;

            WerknemerContainer werknemerContainer = new WerknemerContainer(new WerknemerDAL());
            Werknemer chauffeur = werknemerContainer.GetUserWerknemers(HttpContext.Session.GetInt32("user-id").Value).FirstOrDefault(w => w.WerknemerID == chauffeurID);
            Werknemer bijrijder = werknemerContainer.GetUserWerknemers(HttpContext.Session.GetInt32("user-id").Value).FirstOrDefault(w => w.WerknemerID == bijrijderID);


            DateTime.TryParse(rawDatum, out parsedDatum);
            TimeSpan.TryParse(rawStartTijd, out parsedStartTijd);
            TimeSpan.TryParse(rawEindTijd, out parsedEindTijd);

            RouteRit newRoute = new RouteRit(routeNummer, parsedDatum, chauffeur, bijrijder, parsedStartTijd, parsedEindTijd, bijzonderheden, new RouteDAL());
            newRoute.AddRoute(HttpContext.Session.GetInt32("user-id").Value);
            return Ok("Route Toegevoegd");
        }

        [HttpGet]
        public IActionResult RouteAanpassenView()
        {
            ViewData["Werknemers"] = GetWerknemers();
            return View();
        }   

        [HttpPost]
        public ActionResult RouteAanpassen(int routeId, int routeNummer, string rawDatum, int chauffeurID, int bijrijderID, string rawStartTijd, string rawEindTijd, string bijzonderheden)
        {
            DateTime parsedDatum;
            TimeSpan parsedStartTijd;
            TimeSpan parsedEindTijd;

            WerknemerContainer werknemerContainer = new WerknemerContainer(new WerknemerDAL());
            Werknemer chauffeur = werknemerContainer.GetUserWerknemers(HttpContext.Session.GetInt32("user-id").Value).FirstOrDefault(w => w.WerknemerID == chauffeurID);
            Werknemer bijrijder = werknemerContainer.GetUserWerknemers(HttpContext.Session.GetInt32("user-id").Value).FirstOrDefault(w => w.WerknemerID == bijrijderID);


            DateTime.TryParse(rawDatum, out parsedDatum);
            TimeSpan.TryParse(rawStartTijd, out parsedStartTijd);
            TimeSpan.TryParse(rawEindTijd, out parsedEindTijd);

            RouteRit newRoute = new RouteRit(routeId, routeNummer, parsedDatum, chauffeur, bijrijder, parsedStartTijd, parsedEindTijd, bijzonderheden, new RouteDAL());
            newRoute.UpdateRoute();
            return Ok("Route Aangepast");
        }

        [HttpPost]
        public ActionResult GetSingleRouteByID(int id)
        {
            RouteContainer routeContainer = new RouteContainer(new RouteDAL());
            RouteRit routeRit = routeContainer.GetSingleRoute(HttpContext.Session.GetInt32("user-id").Value, id, new WerknemerDAL(), new WerknemerDAL());
            RouteViewModel route = new RouteViewModel()
            {
                RouteID = routeRit.RouteID,
                RouteNummer = routeRit.RouteNummer,
                Datum = routeRit.Datum,
                Chauffeur = routeRit.Chauffeur,
                BijRijder = routeRit.BijRijder,
                StartTijd = routeRit.StartTijd,
                EindTijd = routeRit.EindTijd,
                AantalUur = routeRit.AantalUur,
                Bijzonderheden = routeRit.Bijzonderheden
            };

            return Ok(route);
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
            RouteRit route = routeContainer.GetRoutes(new WerknemerDAL(), new WerknemerDAL(),HttpContext.Session.GetInt32("user-id").Value).FirstOrDefault(r => r.RouteID == id);

            route.DeleteRoute();
            return Ok("Route Verwijderd");
        }

        [HttpPost]
        public ActionResult GetMonthConclusion(string chosenMonth)
        {
            MonthConclusionContainer monthConclusionContainer = new MonthConclusionContainer(new MonthConclusionDAL());

            MonthConclusion monthConclusion = monthConclusionContainer.CalcMonthConclusion(chosenMonth, new WerknemerDAL(), new WerknemerDAL(), HttpContext.Session.GetInt32("user-id").Value, new RouteDAL());

            return Ok(monthConclusion);
        }
    }
}
