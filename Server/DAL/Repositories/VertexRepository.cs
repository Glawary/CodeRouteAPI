using CodeRoute.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeRoute.DAL.Repositories
{
    public class VertexRepository
    {
        private readonly IContext _context;
        public VertexRepository(IContext context)
        {
            _context = context;
        }


        public IEnumerable<UserVertex> GetAllVertexFromRoute(int routeId, int userId)
        {
            var test = _context.Vertexes.ToList();
            return _context.UserVertexes
                .Where(uv => uv.User.UserId == userId)
                .Include(uv => uv.Vertex)
                .Include(uv => uv.Status)
                .Where(uv => uv.Vertex.RouteId == routeId);
        }

        public IEnumerable<Vertex> GetAllVertexFromRoute(int routeId)
        {
            var test = _context.Vertexes.ToList();
            return _context.Vertexes
                .Where(v => v.RouteId == routeId);
        }


        public IEnumerable<VertexConnection> GetAllVertexConnectionsInRoute(int routeId)
        {
            var t = _context.VertexConnections.ToList();

            return _context.VertexConnections
                .Where(uv => uv.CurrentVertex.RouteId == routeId || uv.CurrentVertex.RouteId == 0)
                .Include(uv => uv.CurrentVertex)
                .Include(uv => uv.PreviousVertex);
        }
    }
}
