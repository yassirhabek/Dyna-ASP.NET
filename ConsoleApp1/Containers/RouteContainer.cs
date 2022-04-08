using DAL.DAL;
using Interfaces.DTO;
using Interfaces.Interface;
using Logic.Models;

namespace Logic.Containers
{
    public class RouteContainer
    {
        private IRouteContainer _iRouteContainer;
        private IRoute _iRoute;

        public RouteContainer(IRouteContainer iRouteContainer)
        {
            _iRouteContainer = iRouteContainer;
            _iRoute = (IRoute)iRouteContainer;
        }

        public List<Route> GetRoute(IWerknemerContainer iWerknemerContainer, IWerknemer iWerknemer)
        {
            List<Route> routes = new List<Route>();
            try
            {
                foreach (RouteDTO routeList in _iRouteContainer.GetAllRoute(iWerknemerContainer.GetAllWerknemers()))
                {
                    Route route = new Route(routeList.RouteID, routeList.RouteNummer, routeList.Datum, new Werknemer(routeList.Chauffeur, iWerknemer),
                        new Werknemer(routeList.BijRijder, iWerknemer), routeList.StartTijd, routeList.EindTijd, _iRoute);
                    routes.Add(route);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return routes;
        }

        public List<Route> GetRouteFromDate(string date, IWerknemerContainer iWerknemerContainer, IWerknemer iWerknemer)
        {
            List<Route> routesFromDate = new List<Route>();

            try
            {
                foreach (RouteDTO routeList in _iRouteContainer.GetRouteFromDate(date, iWerknemerContainer.GetAllWerknemers()))
                {
                    Route route = new Route(routeList.RouteID, routeList.RouteNummer, routeList.Datum, new Werknemer(routeList.Chauffeur, iWerknemer),
                        new Werknemer(routeList.BijRijder, iWerknemer), routeList.StartTijd, routeList.EindTijd, _iRoute);
                    routesFromDate.Add(route);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return routesFromDate;
        }
    }
}