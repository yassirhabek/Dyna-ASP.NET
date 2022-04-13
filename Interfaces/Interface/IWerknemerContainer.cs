using Interfaces.DTO;

namespace Interfaces.Interface
{
    public interface IWerknemerContainer
    {
        List<WerknemerDTO> GetAllWerknemers();
        WerknemerDTO GetSingleWerknemer(int ID);
    }
}