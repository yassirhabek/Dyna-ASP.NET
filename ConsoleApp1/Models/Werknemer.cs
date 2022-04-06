using Interfaces.DTO;

namespace Logic.Models
{
    public class Werknemer 
    {
        public int WerknemerID { get; set; }
        public int NummerPasje { get; set; }
        public string Naam { get; set; }
        public int TelefoonNummer { get; set; }

        public Werknemer()
        {

        }

        public Werknemer(WerknemerDTO dto)
        {
            WerknemerID = dto.WerknemerID;
            NummerPasje = dto.NummerPasje;
            TelefoonNummer = dto.TelefoonNummer;
            Naam = dto.Naam;
        }



        public Werknemer(int werknemerID, int nummerPasje, string naam, int telefoonNummer)
        {
            WerknemerID = werknemerID;
            NummerPasje = nummerPasje;
            Naam = naam;
            TelefoonNummer = telefoonNummer;
        }

        public Werknemer(int nummerPasje, string naam, int telefoonNummer)
        {
            NummerPasje = nummerPasje;
            Naam = naam;
            TelefoonNummer = telefoonNummer;
        }
        public WerknemerDTO WerknemerToDTO()
        {
            WerknemerDTO werknemerDTO = new WerknemerDTO();
            werknemerDTO.WerknemerID = WerknemerID;
            werknemerDTO.Naam = Naam;
            werknemerDTO.NummerPasje = NummerPasje;
            werknemerDTO.TelefoonNummer = TelefoonNummer;
            return werknemerDTO;
        }
    }
}
