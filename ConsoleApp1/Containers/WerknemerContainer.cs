using DAL.DAL;
using Interfaces.DTO;
using Interfaces.Interface;
using Logic.Models;

namespace Logic.Containers
{
    public class WerknemerContainer : IWerknemerContainer
    {
        public List<Werknemer> Werknemers = new List<Werknemer>();

        public WerknemerContainer()
        {
            Werknemers = GetWerknemers();
        }

        public Werknemer GetWerknemer(int ID)
        {
            WerknemerDAL werknemerDAL = new WerknemerDAL();
            try
            {
                WerknemerDTO searchedWerknemer = werknemerDAL.GetWerknemer(ID);
                return new Werknemer(searchedWerknemer.WerknemerID, searchedWerknemer.NummerPasje, searchedWerknemer.Naam, searchedWerknemer.TelefoonNummer);
            }
            catch (Exception)
            {
                return null;
                throw;
            }
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

        public void AddWerknemer(string naam, int werknemerNum, int telefoonNum)
        {
            Werknemer werknemer = new Werknemer(werknemerNum, naam, telefoonNum);
            WerknemerDAL werknemerDAL = new WerknemerDAL();
            werknemerDAL.AddNewWerknemer(werknemer.WerknemerToDTO());
        }

        public void UpdateWerknemer(string naam, int werknemerNum, int telefoonNum, int oldWerknemerID)
        {
            Werknemer newWerknemer = new Werknemer(werknemerNum, naam, telefoonNum);
            WerknemerDAL werknemerDAL = new WerknemerDAL();
            werknemerDAL.ChangeWerknemerData(newWerknemer.WerknemerToDTO(), oldWerknemerID);
        }

        public void DeleteWerknemer(int werknemerID)
        {
            Werknemer werknemer = Werknemers.FirstOrDefault(w => w.WerknemerID == werknemerID);
            WerknemerDAL werknemerDAL = new WerknemerDAL();
            werknemerDAL.DeleteWerknemer(werknemer.WerknemerToDTO());
        }
    }
}
