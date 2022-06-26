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

        public List<RouteRit> GetRoutes(IWerknemerContainer iWerknemerContainer, IWerknemer iWerknemer, int userID)
        {
            List<RouteRit> routes = new List<RouteRit>();
            try
            {
                foreach (RouteDTO routeList in _iRouteContainer.GetAllRoute(iWerknemerContainer.GetUserWerknemers(userID), userID))
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

        public List<RouteRit> GetRoutesFromDate(string rawDate, IWerknemerContainer iWerknemerContainer, IWerknemer iWerknemer, int userID)
        {
            List<RouteRit> routesFromDate = new List<RouteRit>();
            DateTime date;
            DateTime.TryParse(rawDate, out date);

            try
            {
                foreach (RouteDTO routeList in _iRouteContainer.GetRouteByDate(date, iWerknemerContainer.GetUserWerknemers(userID), userID))
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

        public RouteRit GetSingleRoute(int userID, int routeID, IWerknemerContainer iWerknemerContainer, IWerknemer iWerknemer)
        {
            RouteRit routeRit;
            try
            {
                RouteDTO routeDTO = _iRouteContainer.GetSingleRoute(userID, routeID, iWerknemerContainer.GetUserWerknemers(userID));
                RouteRit route = new RouteRit(routeDTO.RouteID, routeDTO.RouteNummer, routeDTO.Datum, new Werknemer(routeDTO.Chauffeur, iWerknemer),
                        new Werknemer(routeDTO.BijRijder, iWerknemer), routeDTO.StartTijd, routeDTO.EindTijd, _iRoute);
                routeRit = route;
            }
            catch (Exception)
            {
                throw;
            }
            return routeRit;
        }
    }
}