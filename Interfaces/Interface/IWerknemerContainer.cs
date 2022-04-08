using Interfaces.DTO;

namespace Interfaces.Interface
{
    public interface IWerknemerContainer
    {
        List<WerknemerDTO> GetAllWerknemers();
        WerknemerDTO GetWerknemer(int ID);
    }
}