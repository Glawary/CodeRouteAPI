namespace CodeRoute.Repositories
{
    public class RouteRepository
    {
        private readonly Context _context;
        public RouteRepository(Context context) 
        {
            _context = context;
        }

        public List<Models.Route> GetAllRoutes()
        {
            return _context.Routes.ToList();
        }
    }
}
