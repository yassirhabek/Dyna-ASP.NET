using DAL.DAL;
using DAL.DTO;
using Logic.Models;
using System.Linq;

namespace ASP.NET.Containers
{
    public class WerknemerContainer
    {
        public List<Werknemer> Werknemers = new List<Werknemer>();

        public WerknemerContainer()
        {
            Werknemers = this.GetWerknemers();
        }

        public List<Werknemer> GetWerknemers()
        {
            WerknemerDAL werknemerDAL = new WerknemerDAL();
            List<Werknemer> werknemers = new List<Werknemer>();
            try
            {
                foreach (var searchedWerknemer in (List<WerknemerDTO>)werknemerDAL.GetAllWerknemers())
                {
                    Werknemer werknemer = new Werknemer(searchedWerknemer.WerknemerID, searchedWerknemer.NummerPasje, searchedWerknemer.Naam);
                    werknemers.Add(werknemer);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return werknemers;
        }
    }
}
