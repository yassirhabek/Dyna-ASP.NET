using Interfaces.DTO;

namespace Logic.Models
{
    public interface IRoute
    {
        TimeSpan AantalUur { get; set; }
        Werknemer BijRijder { get; set; }
        string Bijzonderheden { get; set; }
        Werknemer Chauffeur { get; set; }
        DateTime Datum { get; set; }
        TimeSpan EindTijd { get; set; }
        int RouteID { get; set; }
        int RouteNummer { get; set; }
        TimeSpan StartTijd { get; set; }

        RouteDTO RouteToDTO();
    }
}