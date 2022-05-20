using Interfaces.DTO;
using Interfaces.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unit_Test.Stubs
{
    public class RouteDalStub : IRoute, IRouteContainer
    {
        public List<RouteDTO> RouteDTOs = new List<RouteDTO>();
        public RouteDalStub()
        {
            RouteDTOs.Add(new RouteDTO()
            {
                RouteID = 1,
                RouteNummer = 4214,
                Datum = new DateTime(2022, 6, 27),
                Chauffeur = new WerknemerDTO() { WerknemerID = 1, Naam = "Harry", WerknemerNummer = 12342, TelefoonNummer = 0612842912 },
                BijRijder = new WerknemerDTO() { WerknemerID = 2, Naam = "Lucas", WerknemerNummer = 42341, TelefoonNummer = 0682848211 },
                StartTijd = new TimeSpan(8, 15, 0),
                EindTijd = new TimeSpan(17, 15, 0),
                AantalUur = new TimeSpan(8, 30, 0),
                Bijzonderheden = "geen"
            });

            RouteDTOs.Add(new RouteDTO()
            {
                RouteID = 2,
                RouteNummer = 8223,
                Datum = new DateTime(2022, 6, 28),
                Chauffeur = new WerknemerDTO() { WerknemerID = 1, Naam = "Harry", WerknemerNummer = 12342, TelefoonNummer = 0612842912 },
                BijRijder = new WerknemerDTO() { WerknemerID = 2, Naam = "Lucas", WerknemerNummer = 42341, TelefoonNummer = 0682848211 },
                StartTijd = new TimeSpan(9, 0, 0),
                EindTijd = new TimeSpan(16, 30, 0),
                AantalUur = new TimeSpan(7, 0, 0),
                Bijzonderheden = "assembly stop 4"
            });

            RouteDTOs.Add(new RouteDTO()
            {
                RouteID = 3,
                RouteNummer = 4141,
                Datum = new DateTime(2022, 7, 30),
                Chauffeur = new WerknemerDTO() { WerknemerID = 6, Naam = "Lester", WerknemerNummer = 74443, TelefoonNummer = 0647473927 },
                BijRijder = new WerknemerDTO() { WerknemerID = 8, Naam = "Geert", WerknemerNummer = 53038, TelefoonNummer = 0684739209 },
                StartTijd = new TimeSpan(9, 0, 0),
                EindTijd = new TimeSpan(20, 30, 0),
                AantalUur = new TimeSpan(11, 0, 0),
                Bijzonderheden = "geen"
            });
        }
        public int AddRoute(RouteDTO newRoute, int userID)
        {
            RouteDTOs.Add(newRoute);
            return 1;
        }

        public int DeleteRoute(RouteDTO deleteRoute)
        {
            RouteDTOs.RemoveAll(r => r.RouteID == deleteRoute.RouteID);
            return 1;
        }

        public int UpdateRoute(RouteDTO updateRoute, int oldRouteID)
        {
            RouteDTO newRoute = RouteDTOs.FirstOrDefault(r => r.RouteID == oldRouteID);
            newRoute.RouteID = oldRouteID;
            newRoute.RouteNummer = updateRoute.RouteNummer;
            newRoute.Datum = updateRoute.Datum;
            newRoute.Chauffeur = updateRoute.Chauffeur;
            newRoute.BijRijder = updateRoute.BijRijder;
            newRoute.StartTijd = updateRoute.StartTijd;
            newRoute.EindTijd = updateRoute.EindTijd;
            newRoute.AantalUur = updateRoute.AantalUur;
            newRoute.Bijzonderheden = updateRoute.Bijzonderheden;
            RouteDTOs.RemoveAll(r => r.RouteID == oldRouteID);
            RouteDTOs.Add(newRoute);
            return 1;
        }

        public List<RouteDTO> GetAllRoute(List<WerknemerDTO> lijstWerknemers, int userID)
        {
            return RouteDTOs;
        }

        public List<RouteDTO> GetRouteByDate(DateTime date, List<WerknemerDTO> lijstWerknemers, int userID)
        {
            List<RouteDTO> datumRoute = RouteDTOs.FindAll(r => r.Datum == date);
            return datumRoute;
        }
        
        public List<RouteDTO> GetRoutesByMonth(DateTime smalldate, DateTime bigdate, List<WerknemerDTO> lijstWerknemers, int userID)
        {
            List<RouteDTO> monthRoutes = RouteDTOs.FindAll(r => r.Datum >= smalldate && r.Datum < bigdate);
            return monthRoutes;
        }
    }
}
