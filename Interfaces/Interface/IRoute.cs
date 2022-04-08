using Interfaces.DTO;

namespace Interfaces.Interface
{
    public interface IRoute
    {
        void AddRoute(RouteDTO newRoute);
        void DeleteRoute(RouteDTO deleteRoute);
        void UpdateRoute(RouteDTO updateRoute, RouteDTO oldRoute);
    }
}
