namespace CodeRoute.DTO
{
    public class RoadmapProgress
    {
        public bool IsStarted {  get; set; }
        public float Precent { get; set; }
        public int Finished { get; set; }
        public int InProgress { get; set; }
        public int Skipped { get; set; }
        public int Total { get; set; }
    }
}
