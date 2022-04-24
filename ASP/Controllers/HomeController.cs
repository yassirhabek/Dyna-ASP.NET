﻿using ASP.Models;
using DAL.DAL;
using Logic.Containers;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ASP.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
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

        public IActionResult Index()
        {
            ViewData["Routes"] = GetRoutes();
            ViewData["Werknemers"] = GetWerknemers();
            return View();
        }
    }
}