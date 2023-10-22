using CodeRoute.Services;
using Microsoft.AspNetCore.Mvc;

namespace CodeRoute.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoutesController
    {
        private readonly RouteService _routeService;

        public RoutesController(RouteService routeService) 
        {
            _routeService = routeService;
        }


        [HttpGet(Name = "/get")]
        public IEnumerable<Models.Route> GetList1()
        {
            return _routeService.GetRoutes();
        }
    }
}
