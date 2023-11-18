namespace CodeRoute.DTO
{
    public class Node
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }
        public List<Node>  SecondatyNode { get; set; }
    }
}
