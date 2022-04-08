using DAL.DAL;
using Interfaces.DTO;
using Interfaces.Interface;
using Logic.Models;

namespace Logic.Containers
{
    public class WerknemerContainer
    {
        private IWerknemerContainer _iWerknemerContainer;
        private IWerknemer _iWerknemer;

        public WerknemerContainer(IWerknemerContainer iWerknemerContainer)
        {
            _iWerknemerContainer = iWerknemerContainer;
            _iWerknemer = (IWerknemer)iWerknemerContainer;
        }

        public Werknemer GetWerknemer(int ID)
        {
            try
            {
                WerknemerDTO searchedWerknemer = _iWerknemerContainer.GetWerknemer(ID);
                return new Werknemer(searchedWerknemer.WerknemerID, searchedWerknemer.Naam, searchedWerknemer.NummerPasje, searchedWerknemer.TelefoonNummer, _iWerknemer);
            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }

        public List<Werknemer> GetWerknemers()
        {
            List<Werknemer> werknemers = new List<Werknemer>();
            try
            {
                foreach (var searchedWerknemer in _iWerknemerContainer.GetAllWerknemers())
                {
                    Werknemer werknemer = new Werknemer(searchedWerknemer.WerknemerID, searchedWerknemer.Naam, searchedWerknemer.NummerPasje, searchedWerknemer.TelefoonNummer, _iWerknemer);
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
