namespace CodeRoute.Models
{
    public class UserVertex
    {
        public int VertexId { get; set; }
        public Vertex Vertex { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int StatusId { get; set; }
        public VertexStatus Status { get; set; }
    }
}
