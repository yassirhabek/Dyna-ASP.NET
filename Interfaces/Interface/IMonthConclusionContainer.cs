using Interfaces.DTO;

namespace Interfaces.Interface
{
    public interface IMonthConclusionContainer
    {
        List<RouteDTO> GetRoutesByMonth(DateTime smalldate, DateTime bigdate, List<WerknemerDTO> lijstWerknemers, int userID);
    }
}