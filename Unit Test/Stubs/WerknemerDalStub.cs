using Interfaces.DTO;
using Interfaces.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unit_Test.Stubs
{
    public class WerknemerDalStub : IWerknemer
    {
        public List<WerknemerDTO> werknemerDTOs = new List<WerknemerDTO>();

        public int AddNewWerknemer(WerknemerDTO werknemerDTO)
        {
            werknemerDTOs.Add(werknemerDTO);
            return 1;
        }

        public int UpdateWerknemer(WerknemerDTO werknemerDTO, int oldWerknemerID)
        {
            throw new NotImplementedException();
        }

        public void DeleteWerknemer(WerknemerDTO werknemer)
        {
            throw new NotImplementedException();
        }
    }
}
