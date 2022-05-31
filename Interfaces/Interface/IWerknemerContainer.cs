using Interfaces.DTO;

namespace Interfaces.Interface
{
    public interface IWerknemerContainer
    {
        List<WerknemerDTO> GetAllWerknemers();
        List<WerknemerDTO> GetUserWerknemers(int userID);
        WerknemerDTO GetSingleWerknemer(int ID);
    }
}