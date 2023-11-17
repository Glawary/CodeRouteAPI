using CodeRoute.DAL.Repositories;
using CodeRoute.DTO;
using CodeRoute.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace CodeRoute.Services
{
    public class RouteService
    {
        private static readonly int specialValue = 1;

        private readonly RouteRepository _routeRepository;
        private readonly VertexRepository _vertexRepository;
        public RouteService(RouteRepository routeRepository, VertexRepository vertexRepository) 
        {
            _routeRepository = routeRepository;
            _vertexRepository = vertexRepository;
        }

        public List<Models.Route> GetRoutes() 
        {
            var result = _routeRepository.GetAllRoutes();
            return result;
        }

        internal bool AddRoute(Roadmap roadmap)
        {
            Models.Route route = new Models.Route()
            {
                Title = roadmap.Title,
                Desctiption = roadmap.Desctiption,
                MarkDownPage = ""
            };

            return _routeRepository.AddRoute(route);
        }

        internal RouteInfo GetRouteByIdForUser(int routId, int userId)
        {
            return null;
        }

        internal RouteInfo GetRouteById(int routId)
        {
            Models.Route route = _routeRepository.GetRouteById(routId);

            if (route == null) return null;

            Roadmap map = new Roadmap()
            {
                Title = route.Title,
                Desctiption = route.Desctiption,
            };

            List<Vertex> vertices = _vertexRepository.GetAllVertexFromRoute(routId).ToList();
            if (vertices.Count == 0)
            {
                vertices = _vertexRepository.GetAllVertexFromRoute(routId).ToList();
            }

            List<VertexConnection> connections = _vertexRepository.GetAllVertexConnectionsInRoute(routId).ToList();

            List<Node> nodes = GetNodeList(vertices, connections);

            RoadmapProgress progress = CalcProgress(nodes);

            RouteInfo info = new RouteInfo()
            {
                Roadmap = map,
                Nodes = nodes,
                Progress = progress,
            };

            return info;
        }

        private List<Node> GetNodeList(List<Vertex> vertices, List<VertexConnection> connections)
        {
            var mainAxisIds = connections.Select(c => c.CurrentVertexId).Distinct().ToList();
            var mainAxis = connections.Select(c => c.CurrentVertex).Distinct().ToList();

            List<Node> nodes = new List<Node>();

            foreach (var vertex in mainAxis)
            {
                Node node = NodeFromVertex(vertex, vertices);

                node.SecondatyNode = new List<Node>();

                List<Vertex> prevVertices = connections
                    .Where(c => c.CurrentVertexId == vertex.VertexId && 
                                !mainAxisIds.Contains(c.PreviousVertexId) && 
                                c.PreviousVertexId != specialValue)
                    .Select(c => c.PreviousVertex)
                    .ToList();

                foreach (var vert in prevVertices)
                {
                    node.SecondatyNode.Add(NodeFromVertex(vert, vertices));
                }

                nodes.Add(node);
            }

            return nodes;
        }

        private Node NodeFromVertex(Vertex vertex, IEnumerable<Vertex> vertices)
        {
            VertexStatus stat = new VertexStatus() { StatusId = 1, StatusName = "Не изучено" };
            Node node = new Node()
            {
                Id = vertex.VertexId,
                Title = vertex.Name,
                Status = stat.StatusName,
            };

            return node;
        }

        private RoadmapProgress CalcProgress(List<Node> nodes)
        {
            RoadmapProgress progress = new RoadmapProgress();

            if (nodes == null) return progress;

            foreach (var node in nodes)
            {
                if (node.Status == "InProgress") progress.InProgress++;
                if (node.Status == "Skip") progress.Skipped++;
                if (node.Status == "Done") progress.Finished++;
                progress.Total++;

                RoadmapProgress secondaryProg = CalcProgress(node.SecondatyNode);

                progress.InProgress += secondaryProg.InProgress;
                progress.Skipped += secondaryProg.Skipped;
                progress.Finished += secondaryProg.Finished;
                progress.Total += secondaryProg.Total;
            }

            if (progress.Finished == 0)
            {
                progress.Precent = 0.0f;
            }
            else
            {
                progress.Precent = progress.Finished * 1.0f / progress.Total;
            }

            return progress;
        }
    }
}
