using DAL.DAL;
using Interfaces.DTO;
using Interfaces.Interface;
using System;

namespace Logic.Models
{
    public class Route
    {
        private IRoute _iRoute;
        public int RouteID { get; set; }
        public int RouteNummer { get; set; }
        public DateTime Datum { get; set; }
        public Werknemer Chauffeur { get; set; }
        public Werknemer BijRijder { get; set; }
        public TimeSpan StartTijd { get; set; }
        public TimeSpan EindTijd { get; set; }
        public TimeSpan AantalUur { get; set; }
        public string Bijzonderheden { get; set; }

        public Route()
        {

        }
        public Route(int routeID, int routeNummer, DateTime datum, Werknemer chauffeur, Werknemer bijRijder, TimeSpan startTijd, TimeSpan eindTijd, string bijzonderheden, IRoute iRoute)
        {
            RouteID = routeID;
            RouteNummer = routeNummer;
            Datum = datum;
            Chauffeur = chauffeur;
            BijRijder = bijRijder;
            StartTijd = startTijd;
            EindTijd = eindTijd;
            Bijzonderheden = bijzonderheden;
            _iRoute = iRoute;

            AantalUur = this.totaalAantalUur(startTijd, eindTijd);
        }

        public Route(int routeID, int routeNummer, DateTime datum, Werknemer chauffeur, Werknemer bijRijder, TimeSpan startTijd, TimeSpan eindTijd, IRoute iRoute)
        {
            RouteID = routeID;
            RouteNummer = routeNummer;
            Datum = datum;
            Chauffeur = chauffeur;
            BijRijder = bijRijder;
            StartTijd = startTijd;
            EindTijd = eindTijd;
            _iRoute = iRoute;

            AantalUur = totaalAantalUur(startTijd, eindTijd);
        }

        public Route(int routeNummer, DateTime datum, Werknemer chauffeur, Werknemer bijRijder, TimeSpan startTijd, TimeSpan eindTijd, string bijzonderheden, IRoute iRoute)
        {
            RouteNummer = routeNummer;
            Datum = datum;
            Chauffeur = chauffeur;
            BijRijder = bijRijder;
            StartTijd = startTijd;
            EindTijd = eindTijd;
            Bijzonderheden = bijzonderheden;
            _iRoute = iRoute;

            AantalUur = totaalAantalUur(startTijd, eindTijd);
        }

        public Route(int routeNummer, DateTime datum, Werknemer chauffeur, Werknemer bijRijder, TimeSpan startTijd, TimeSpan eindTijd, IRoute iRoute)
        {
            RouteNummer = routeNummer;
            Datum = datum;
            Chauffeur = chauffeur;
            BijRijder = bijRijder;
            StartTijd = startTijd;
            EindTijd = eindTijd;
            _iRoute = iRoute; 

            AantalUur = totaalAantalUur(startTijd, eindTijd);
        }

        public void AddRoute(Route newRoute)
        {
            _iRoute.AddRoute(newRoute.RouteToDTO());
        }

        public void UpdateRoute(Route updateRoute, Route oldRoute)
        {
            _iRoute.UpdateRoute(updateRoute.RouteToDTO(), oldRoute.RouteToDTO());
        }

        public void DeleteRoute(Route deleteRoute)
        {
            _iRoute.DeleteRoute(deleteRoute.RouteToDTO());
        }
        private TimeSpan totaalAantalUur(TimeSpan startTijd, TimeSpan eindTijd)
        {
            string rawHalfuur = "00:30:00";
            TimeSpan halfUur = TimeSpan.Parse(rawHalfuur);

            TimeSpan aantalUur = eindTijd - startTijd - halfUur;
            return aantalUur;
        }

        private RouteDTO RouteToDTO()
        {
            RouteDTO routeDTO = new RouteDTO();
            routeDTO.RouteID = RouteID;
            routeDTO.RouteNummer = RouteNummer;
            routeDTO.Datum = Datum;
            routeDTO.Chauffeur = Chauffeur.WerknemerToDTO();
            routeDTO.BijRijder = BijRijder.WerknemerToDTO();
            routeDTO.StartTijd = StartTijd;
            routeDTO.EindTijd = EindTijd;
            routeDTO.Bijzonderheden = Bijzonderheden;
            return routeDTO;
        }
    }
}
