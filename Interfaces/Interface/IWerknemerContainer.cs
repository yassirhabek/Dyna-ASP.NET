using Interfaces.DTO;

namespace Interfaces.Interface
{
    public interface IWerknemerContainer
    {
        List<WerknemerDTO> GetAllWerknemers(int userID);
        WerknemerDTO GetSingleWerknemer(int ID);
    }
}