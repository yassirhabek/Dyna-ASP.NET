using Interfaces.DTO;

namespace Interfaces.Interface
{
    public interface IWerknemer
    {
        int AddNewWerknemer(WerknemerDTO werknemerNieuw);
        int UpdateWerknemer(WerknemerDTO changedWerknemer, int oldWerknemerID);
        int DeleteWerknemer(WerknemerDTO werknemer);
    }
}