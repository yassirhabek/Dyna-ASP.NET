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
                foreach (RouteDTO routeList in _iRouteContainer.GetAllRoute(iWerknemerContainer.GetAllWerknemers(userID), userID))
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
                foreach (RouteDTO routeList in _iRouteContainer.GetRouteByDate(date, iWerknemerContainer.GetAllWerknemers(userID), userID))
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

        public List<RouteRit> GetRoutesByMonth(string monthYearRaw, IWerknemerContainer iWerknemerContainer, IWerknemer iWerknemer, int userID)
        {
            string[] monthYearRawSplitted = monthYearRaw.Split('-');
            int[] monthYear = Array.ConvertAll(monthYearRawSplitted, int.Parse);
            int year = monthYear[0];
            int month = monthYear[1];
            List<RouteRit> routeByMonth = new List<RouteRit>();
            DateTime smalldate = new DateTime(year, month, 1);
            DateTime bigdate = new DateTime(year, month++, 1);

            try
            {
                foreach (RouteDTO routeDTO in _iRouteContainer.GetRoutesByMonth(smalldate, bigdate, iWerknemerContainer.GetAllWerknemers(userID), userID))
                {
                    RouteRit route = new RouteRit(routeDTO.RouteID, routeDTO.RouteNummer, routeDTO.Datum, new Werknemer(routeDTO.Chauffeur, iWerknemer),
                            new Werknemer(routeDTO.BijRijder, iWerknemer), routeDTO.StartTijd, routeDTO.EindTijd, _iRoute);
                    routeByMonth.Add(route);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return routeByMonth;
        }
    }
}