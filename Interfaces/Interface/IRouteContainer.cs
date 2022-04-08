using Interfaces.DTO;

namespace Interfaces.Interface
{
    public interface IRouteContainer
    {
        List<RouteDTO> GetAllRoute(List<WerknemerDTO> lijstWerknemers);
        List<RouteDTO> GetRouteFromDate(string date, List<WerknemerDTO> lijstWerknemers);
    }
}