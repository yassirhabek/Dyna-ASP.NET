using Interfaces.DTO;

namespace Interfaces.Interface
{
    public interface IRouteContainer
    {
        void AddRoute(RouteDTO newRoute);
        void DeleteRoute(RouteDTO deleteRoute);
        List<RouteDTO> GetRoute();
        List<RouteDTO> GetRouteFromDate(string date);
        void UpdateRoute(RouteDTO updateRoute, RouteDTO oldRoute);
    }
}