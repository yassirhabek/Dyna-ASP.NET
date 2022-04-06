using Interfaces.DTO;

namespace Interfaces.Interface
{
    public interface IWerknemerContainer
    {
        void AddWerknemer(string naam, int werknemerNum, int telefoonNum);
        void DeleteWerknemer(int werknemerID);
        WerknemerDTO GetWerknemer(int ID);
        List<WerknemerDTO> GetWerknemers();
        void UpdateWerknemer(string naam, int werknemerNum, int telefoonNum, int oldWerknemerID);
    }
}