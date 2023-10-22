
namespace CodeRoute.Models
{
    public class UserRoute
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int RouteId { get; set; }
        public Route Route { get; set; }

        public int RouteStatusId { get; set; }
        public RouteStatus RouteStatus { get; set; }
    }
}
