﻿using Interfaces.DTO;

namespace Interfaces.Interface
{
    public interface IRouteContainer
    {
        List<RouteDTO> GetAllRoute(List<WerknemerDTO> lijstWerknemers, int userID);
        List<RouteDTO> GetRouteByDate(DateTime date, List<WerknemerDTO> lijstWerknemers, int userID);
        RouteDTO GetSingleRoute(int userID, int routeID, List<WerknemerDTO> lijstWerknemers);
    }
}