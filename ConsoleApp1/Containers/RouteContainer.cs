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

        public List<RouteRit> GetRoutes(IWerknemerContainer iWerknemerContainer, IWerknemer iWerknemer)
        {
            List<RouteRit> routes = new List<RouteRit>();
            try
            {
                foreach (RouteDTO routeList in _iRouteContainer.GetAllRoute(iWerknemerContainer.GetAllWerknemers()))
                {
                    RouteRit route = new RouteRit(routeList.RouteID, routeList.RouteNummer, routeList.Datum, new Werknemer(routeList.Chauffeur, iWerknemer),
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

        public List<RouteRit> GetRoutesFromDate(string rawDate, IWerknemerContainer iWerknemerContainer, IWerknemer iWerknemer)
        {
            List<RouteRit> routesFromDate = new List<RouteRit>();
            DateTime date;
            DateTime.TryParse(rawDate, out date);

            try
            {
                foreach (RouteDTO routeList in _iRouteContainer.GetRouteFromDate(date, iWerknemerContainer.GetAllWerknemers()))
                {
                    RouteRit route = new RouteRit(routeList.RouteID, routeList.RouteNummer, routeList.Datum, new Werknemer(routeList.Chauffeur, iWerknemer),
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