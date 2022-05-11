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
        }
        public int AddRoute(RouteDTO newRoute)
        {
            RouteDTOs.Add(newRoute);
            return 1;
        }

        public int DeleteRoute(RouteDTO deleteRoute)
        {
            throw new NotImplementedException();
        }

        public int UpdateRoute(RouteDTO updateRoute, int oldRouteID)
        {
            throw new NotImplementedException();
        }

        public List<RouteDTO> GetAllRoute(List<WerknemerDTO> lijstWerknemers)
        {
            throw new NotImplementedException();
        }

        public List<RouteDTO> GetRouteFromDate(DateTime date, List<WerknemerDTO> lijstWerknemers)
        {
            throw new NotImplementedException();
        }
    }
}
