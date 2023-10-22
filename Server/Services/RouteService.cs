using CodeRoute.Repositories;

namespace CodeRoute.Services
{
    public class RouteService
    {
        private readonly RouteRepository _routeRepository;
        public RouteService(RouteRepository routeRepository) 
        {
            _routeRepository = routeRepository;
        }

        public List<Models.Route> GetRoutes() 
        {
            var result = _routeRepository.GetAllRoutes();
            return result;
        }
    }
}
