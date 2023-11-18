using CodeRoute.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeRoute.DAL
{
    public class TestContext : DbContext, IContext
    {
        public TestContext(DbContextOptions<TestContext> options) : base(options)
        {
            if (this.Routes.Count() == 0)
            {
                ResetTestDB();
            }
        }

        private void ResetTestDB()
        {
            this.AddRoutePresets();
            this.AddVertexPresets();
            this.AddVertexConnectionsPresets();
            this.AddRouteStatusPresets();
            this.AddVertexStatusPresets();

            this.SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRoute>().HasKey(ur => new { ur.UserId, ur.RouteId });
            modelBuilder.Entity<RelatedRoutes>().HasKey(ur => new { ur.CurrentRouteId, ur.RelatedRouteId });
            modelBuilder.Entity<UserVertex>().HasKey(ur => new { ur.UserId, ur.VertexId });
            modelBuilder.Entity<VertexConnection>().HasKey(ur => new { ur.CurrentVertexId, ur.PreviousVertexId });

            modelBuilder.Entity<User>().HasAlternateKey(u => u.Email);
            modelBuilder.Entity<Models.Route>().HasAlternateKey(r => r.Title);
        }


        public DbSet<User> Users { get; set; }
        public DbSet<Models.Route> Routes { get; set; }
        public DbSet<UserRoute> UserRoutes { get; set; }
        public DbSet<RouteStatus> RouteStatuses { get; set; }
        public DbSet<RelatedRoutes> RelatedRoutes { get; set; }
        public DbSet<UserVertex> UserVertexes { get; set; }
        public DbSet<Vertex> Vertexes { get; set; }
        public DbSet<VertexStatus> VertexStatuses { get; set; }
        public DbSet<VertexConnection> VertexConnections { get; set; }
    }
}
