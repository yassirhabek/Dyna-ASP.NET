using DAL.DAL;
using DAL.DTO;
using Logic.Models;

namespace Logic.Containers
{
    public class RouteContainer
    {
        public List<Route> Routes = new List<Route>();

        public void AddRoute(Route newRoute)
        {
            RouteDAL routeDAL = new RouteDAL();
            routeDAL.AddRoute(newRoute);
        }
    }
}
