using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.Extensions.Logging;
using CodeRoute.Logs;

namespace CodeRoute.DAL.Repositories
{
    public class RouteRepository
    {
        private readonly IContext _context;
        private Logger<RouteRepository> _logger;

        public RouteRepository(IContext context, Logger<RouteRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public List<Models.Route> GetAllRoutes()
        {
            _logger.LogInformation("Get all routes request", null);
            try
            {
                return _context.Routes.Skip(1).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error with add new route in bd", null);
            }

            return null;
        }

        internal Models.Route? GetRouteById(int id)
        {
            _logger.LogInformation("Get route by ID request", null);
            return _context.Routes.FirstOrDefault(route => route.RouteId == id && id != 1);
        }

        public bool AddRoute(Models.Route route)
        {
            _logger.LogInformation("Add route request", null);
            try
            {
                _context.Routes.Add(route);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.InnerException, "Error with add new route in bd", null);
                return false;
            }

            return true;
        }

    }
}
