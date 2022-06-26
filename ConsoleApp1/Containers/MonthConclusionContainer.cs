using DAL.DAL;
using Interfaces.DTO;
using Interfaces.Interface;
using Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Containers
{
    public class MonthConclusionContainer
    {
        private IMonthConclusionContainer _iMonthConclusionContainer;
        public MonthConclusionContainer(IMonthConclusionContainer iMonthConclusionContainer)
        {
            _iMonthConclusionContainer = iMonthConclusionContainer;
        }

        

        public MonthConclusion CalcMonthConclusion(string monthYearRaw, IWerknemerContainer iWerknemerContainer, IWerknemer iWerknemer, int userID, IRoute iRoute)
        {
            string[] monthYearRawSplitted = monthYearRaw.Split('-');
            int[] monthYear = Array.ConvertAll(monthYearRawSplitted, int.Parse);
            int year = monthYear[0];
            int month = monthYear[1];
            List<RouteRit> routeByMonth = new List<RouteRit>();
            DateTime smalldate = new DateTime(year, month, 1);
            DateTime bigdate = new DateTime(year, month + 1, 1);

            try
            {
                foreach (RouteDTO routeDTO in _iMonthConclusionContainer.GetRoutesByMonth(smalldate, bigdate, iWerknemerContainer.GetUserWerknemers(userID), userID))
                {
                    RouteRit route = new RouteRit(routeDTO.RouteID, routeDTO.RouteNummer, routeDTO.Datum, new Werknemer(routeDTO.Chauffeur, iWerknemer),
                            new Werknemer(routeDTO.BijRijder, iWerknemer), routeDTO.StartTijd, routeDTO.EindTijd, iRoute);
                    routeByMonth.Add(route);
                }
            }
            catch (Exception)
            {
                throw;
            }

            MonthConclusion monthConclusion = new MonthConclusion();

            foreach (RouteRit routeRit in routeByMonth)
            {
                monthConclusion.AantalRoutesGereden += 1;
                monthConclusion.AantalUren += routeRit.AantalUur;
            }
            string urenDisplay = Convert.ToString((int)monthConclusion.AantalUren.TotalHours) + ":" + monthConclusion.AantalUren.Minutes.ToString();
            monthConclusion.AantalUrenDisplay = urenDisplay;
            return monthConclusion;
        }

        /*public List<RouteRit> GetRoutesByMonth(string monthYearRaw, IWerknemerContainer iWerknemerContainer, IWerknemer iWerknemer, int userID, IRoute iRoute)
            {
                string[] monthYearRawSplitted = monthYearRaw.Split('-');
                int[] monthYear = Array.ConvertAll(monthYearRawSplitted, int.Parse);
                int year = monthYear[0];
                int month = monthYear[1];
                List<RouteRit> routeByMonth = new List<RouteRit>();
                DateTime smalldate = new DateTime(year, month, 1);
                DateTime bigdate = new DateTime(year, month + 1, 1);

                try
                {
                    foreach (RouteDTO routeDTO in _iMonthConclusionContainer.GetRoutesByMonth(smalldate, bigdate, iWerknemerContainer.GetAllWerknemers(userID), userID))
                    {
                        RouteRit route = new RouteRit(routeDTO.RouteID, routeDTO.RouteNummer, routeDTO.Datum, new Werknemer(routeDTO.Chauffeur, iWerknemer),
                                new Werknemer(routeDTO.BijRijder, iWerknemer), routeDTO.StartTijd, routeDTO.EindTijd, iRoute);
                        routeByMonth.Add(route);
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                return routeByMonth;
            }*/
    }
}
