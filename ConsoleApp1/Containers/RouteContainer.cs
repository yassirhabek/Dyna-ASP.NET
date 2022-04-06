using DAL.DAL;
using Interfaces.DTO;
using Logic.Models;

namespace Logic.Containers
{
    public class RouteContainer
    {
        public List<Route> Routes = new List<Route>();

        public RouteContainer()
        {
            Routes = GetRoute();
        }

        public void AddRoute(Route newRoute)
        {
            RouteDAL routeDAL = new RouteDAL();
            routeDAL.AddRoute(newRoute.RouteToDTO());
        }

        public List<Route> GetRoute()
        {
            WerknemerDAL werknemerDAL = new WerknemerDAL();
            RouteDAL routeDAL = new RouteDAL();
            List<Route> routes = new List<Route>();
            try
            {
                foreach (RouteDTO routeList in routeDAL.GetAllRoute(werknemerDAL.GetAllWerknemers()))
                {
                    Route route = new Route(routeList.RouteID, routeList.RouteNummer, routeList.Datum, new Werknemer(routeList.Chauffeur),
                        new Werknemer(routeList.BijRijder), routeList.StartTijd, routeList.EindTijd);
                    routes.Add(route);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return routes;
        }

        public List<Route> GetRouteFromDate(string date)
        {
            WerknemerDAL werknemerDAL = new WerknemerDAL();
            RouteDAL routeDAL = new RouteDAL();
            List<Route> routesFromDate = new List<Route>();

            try
            {
                foreach (RouteDTO routeList in routeDAL.GetRouteFromDate(date, werknemerDAL.GetAllWerknemers()))
                {
                    Route route = new Route(routeList.RouteID, routeList.RouteNummer, routeList.Datum, new Werknemer(routeList.Chauffeur),
                        new Werknemer(routeList.BijRijder), routeList.StartTijd, routeList.EindTijd);
                    routesFromDate.Add(route);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return routesFromDate;
        }

        public void UpdateRoute(Route updateRoute, Route oldRoute)
        {
            RouteDAL routeDAL = new RouteDAL();
            routeDAL.UpdateRoute(updateRoute.RouteToDTO(), oldRoute.RouteToDTO());
        }

        public void DeleteRoute(Route deleteRoute)
        {
            RouteDAL routeDAL = new RouteDAL();
            routeDAL.DeleteRoute(deleteRoute.RouteToDTO());
        }
    }
}