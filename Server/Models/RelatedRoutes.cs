namespace CodeRoute.Models
{
    public class RelatedRoutes
    {
        public int CurrentRouteId { get; set; }
        public Route CurrentRoute { get; set; }

        public int RelatedRouteId { get; set; }
        public Route RelatedRoute { get; set; }
    }
}
