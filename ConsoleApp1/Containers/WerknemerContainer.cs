using DAL.DAL;
using DAL.DTO;
using Logic.Models;

namespace Logic.Containers
{
    public class WerknemerContainer
    {
        public List<Werknemer> Werknemers = new List<Werknemer>();

        public WerknemerContainer()
        {
            Werknemers = GetWerknemers();
        }

        public List<Werknemer> GetWerknemers()
        {
            WerknemerDAL werknemerDAL = new WerknemerDAL();
            List<Werknemer> werknemers = new List<Werknemer>();
            try
            {
                foreach (var searchedWerknemer in werknemerDAL.GetAllWerknemers())
                {
                    Werknemer werknemer = new Werknemer(searchedWerknemer.WerknemerID, searchedWerknemer.NummerPasje, searchedWerknemer.Naam, searchedWerknemer.TelefoonNummer);
                    werknemers.Add(werknemer);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return werknemers;
        }

        public void AddWerknemer(Werknemer werknemer)
        {
            WerknemerDAL werknemerDAL = new WerknemerDAL();
            werknemerDAL.AddNewWerknemer(WerknemerToDTO(werknemer));
        }

        private WerknemerDTO WerknemerToDTO(Werknemer werknemer)
        {
            WerknemerDTO werknemerDTO = new WerknemerDTO();
            werknemerDTO.WerknemerID = werknemer.WerknemerID;
            werknemerDTO.Naam = werknemer.Naam;
            werknemerDTO.NummerPasje = werknemer.NummerPasje;
            werknemerDTO.TelefoonNummer = werknemer.TelefoonNummer;
            return werknemerDTO;
        }
    }
}
