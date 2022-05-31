using Interfaces.DTO;
using Interfaces.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unit_Test.Stubs
{
    public class WerknemerDalStub : IWerknemer, IWerknemerContainer
    {
        public List<WerknemerDTO> werknemerDTOs = new List<WerknemerDTO>();

        public WerknemerDalStub()
        {
            werknemerDTOs.Add(new WerknemerDTO()
            {
                WerknemerID = 4,
                Naam = "Daniel",
                WerknemerNummer = 123321,
                TelefoonNummer = 0619244212
            });

            werknemerDTOs.Add(new WerknemerDTO()
            {
                WerknemerID = 5,
                Naam = "Matthijs",
                WerknemerNummer = 097212,
                TelefoonNummer = 0672249187
            });

            werknemerDTOs.Add(new WerknemerDTO()
            {
                WerknemerID = 6,
                Naam = "Hugo",
                WerknemerNummer = 344874,
                TelefoonNummer = 0622000331
            });
        }

        public int AddNewWerknemer(WerknemerDTO werknemerDTO, int userID)
        {
            werknemerDTOs.Add(werknemerDTO);
            return 1;
        }

        public int UpdateWerknemer(WerknemerDTO werknemerDTO, int oldWerknemerID)
        {
            WerknemerDTO newWerknemer = werknemerDTOs.FirstOrDefault(w => w.WerknemerID == oldWerknemerID);
            newWerknemer.WerknemerID = oldWerknemerID;
            newWerknemer.Naam = werknemerDTO.Naam;
            newWerknemer.WerknemerNummer = werknemerDTO.WerknemerNummer;
            newWerknemer.TelefoonNummer= werknemerDTO.TelefoonNummer;
            werknemerDTOs.RemoveAll(w => w.WerknemerID == oldWerknemerID);
            werknemerDTOs.Add(newWerknemer);

            return 1;
        }

        public int DeleteWerknemer(WerknemerDTO werknemer)
        {
            werknemerDTOs.RemoveAll(w => w.WerknemerID == werknemer.WerknemerID);
            return 1;
        }

        public List<WerknemerDTO> GetUserWerknemers(int userID)
        {
            return werknemerDTOs;
        }

        public WerknemerDTO GetSingleWerknemer(int ID)
        {
            WerknemerDTO werknemerDTO = werknemerDTOs.FirstOrDefault(w => w.WerknemerID == ID);
            return werknemerDTO;
        }
    }
}
