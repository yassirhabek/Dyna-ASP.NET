using Interfaces.DTO;

namespace Interfaces.Interface
{
    public interface IWerknemer
    {
        void AddNewWerknemer(WerknemerDTO werknemerNieuw);
        void ChangeWerknemerData(WerknemerDTO changedWerknemer, int oldWerknemerID);
        void DeleteWerknemer(WerknemerDTO werknemer);
    }
}