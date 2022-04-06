using Interfaces.DTO;

namespace Interfaces.Interface
{
    public interface IWerknemer
    {
        string Naam { get; set; }
        int NummerPasje { get; set; }
        int TelefoonNummer { get; set; }
        int WerknemerID { get; set; }

        WerknemerDTO WerknemerToDTO();
    }
}