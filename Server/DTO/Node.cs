namespace CodeRoute.DTO
{
    public class Node
    {
        public string Title { get; set; }
        public string Status { get; set; }
        public List<Node>  SecondatyNode { get; set; }
    }
}
