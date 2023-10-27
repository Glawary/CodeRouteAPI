using Microsoft.AspNetCore.Server.IIS.Core;

namespace CodeRoute.Repositories
{
    public class RouteRepository
    {
        private readonly Context _context;
        public RouteRepository(Context context) 
        {
            _context = context;
        }

        public List<Models.Route> GetAllRoutes()
        {
            return _context.Routes.ToList();
        }

        internal Models.Route GetRouteById(int id)
        {
            return _context.Routes.FirstOrDefault(route => route.RouteId == id);
        }

        public bool AddRoute(Models.Route route)
        {
            try
            {
                _context.Add(route);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

    }
}
