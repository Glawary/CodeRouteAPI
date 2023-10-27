using CodeRoute.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeRoute.Repositories
{
    public class VertexRepository
    {
        private readonly Context _context;
        public VertexRepository(Context context)
        {
            _context = context;
        }


        public IEnumerable<UserVertex> GetAllVertexFromRoute(int routeId, int userId)
        {
            var test = _context.Vetexes.ToList();
            return _context.UserVertexes
                .Where(uv => uv.User.UserId == userId)
                .Include(uv => uv.Vertex)
                .Include(uv => uv.Status)
                .Where(uv => uv.Vertex.RouteId == routeId);
        }


        public IEnumerable<VertexConnection> GetAllVertexConnectionsInRoute(int routeId)
        {
            return _context.VertexConnections
                .Where(uv => uv.CurrentVertex.RouteId == routeId || uv.CurrentVertex.RouteId == 0)
                .Include(uv => uv.CurrentVertex)
                .Include(uv => uv.PreviousVertex);
        }
    }
}
