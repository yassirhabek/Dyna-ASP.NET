using Interfaces.DTO;

namespace Interfaces.Interface
{
    public interface IRoute
    {
        int AddRoute(RouteDTO newRoute, int userID);
        int DeleteRoute(RouteDTO deleteRoute);
        int UpdateRoute(RouteDTO updateRoute);
    }
}
