using CodeRoute.DTO;
using CodeRoute.Models;
using CodeRoute.Repositories;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace CodeRoute.Services
{
    public class RouteService
    {
        private static readonly int specialValue = 3228;

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

        internal RouteInfo GetRouteById(int routId, int userId)
        {
            Models.Route route = _routeRepository.GetRouteById(routId);
            Roadmap map = new Roadmap()
            {
                Title = route.Title,
                Desctiption = route.Desctiption,
            };

            List<UserVertex> vertices = _vertexRepository.GetAllVertexFromRoute(routId, userId).ToList();
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

        private List<Node> GetNodeList(List<UserVertex> vertices, List<VertexConnection> connections)
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

        private Node NodeFromVertex(Vertex vertex, IEnumerable<UserVertex> vertices)
        {
            VertexStatus stat = vertices.FirstOrDefault(v => v.VertexId == vertex.VertexId).Status;
            Node node = new Node()
            {
                Title = vertex.Name,
                Status = stat.StatusName,
            };

            return node;
        }

        private RoadmapProgress CalcProgress(List<Node> nodes)
        {
            RoadmapProgress progress = new RoadmapProgress();

            if (nodes == null)
            {
                return progress;
            }

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

            progress.Precent = progress.Finished * 1.0f / progress.Total;

            return progress;
        }
    }
}
