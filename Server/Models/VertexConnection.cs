namespace CodeRoute.Models
{
    public class VertexConnection
    {
        public int CurrentVertexId { get; set; }
        public Vertex CurrentVertex { get; set; }

        public int PreviousVertexId { get; set; }
        public Vertex PreviousVertex { get; set; }
    }
}
