using Interfaces.DTO;

namespace Interfaces.Interface
{
    public interface IRoute
    {
        TimeSpan AantalUur { get; set; }
        WerknemerDTO BijRijder { get; set; }
        string Bijzonderheden { get; set; }
        WerknemerDTO Chauffeur { get; set; }
        DateTime Datum { get; set; }
        TimeSpan EindTijd { get; set; }
        int RouteID { get; set; }
        int RouteNummer { get; set; }
        TimeSpan StartTijd { get; set; }

        RouteDTO RouteToDTO();
    }
}