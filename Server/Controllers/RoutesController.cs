using CodeRoute.DTO;
using CodeRoute.Models;
using CodeRoute.Services;
using Microsoft.AspNetCore.Mvc;

namespace CodeRoute.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoutesController : ControllerBase
    {
        private readonly RouteService _routeService;

        public RoutesController(RouteService routeService) 
        {
            _routeService = routeService;
        }


        [HttpGet]
        public ActionResult<IEnumerable<Models.Route>> GetList()
        {
            return _routeService.GetRoutes();
        }

        [HttpGet("{routId}/{userId}", Name = "/get")]
        public ActionResult<RouteInfo> GetRouteInfoById(int routId, int userId)
        {
            var result = _routeService.GetRouteByIdForUser(routId, userId);

            if (result == null)
            {
                return NotFound("Такого нету");
            }

            return Ok(result);
        }

        [HttpGet("{routId}", Name = "/get2")]
        public ActionResult<RouteInfo> GetRouteInfoById(int routId)
        {
            var result = _routeService.GetRouteById(routId);

            if (result == null)
            {
                return NotFound("Такого нету");
            }

            return Ok(result);
        }

        [HttpPost]
        public ActionResult<bool> AddRoute([FromBody] Roadmap roadmap)
        {
            bool result = _routeService.AddRoute(roadmap);

            if (result)
            {
                return Ok(result);
            }

            return BadRequest();
        }
    }
}
