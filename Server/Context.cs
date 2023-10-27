using CodeRoute.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeRoute
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options): base(options)
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRoute>().HasKey(ur => new { ur.UserId, ur.RouteId});
            modelBuilder.Entity<RelatedRoutes>().HasKey(ur => new { ur.CurrentRouteId, ur.RelatedRouteId});
            modelBuilder.Entity<UserVertex>().HasKey(ur => new { ur.UserId, ur.VertexId});
            modelBuilder.Entity<VertexConnection>().HasKey(ur => new { ur.CurrentVertexId, ur.PreviousVertexId});


            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
            modelBuilder.Entity<Models.Route>().HasIndex(r => r.Title).IsUnique();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Models.Route> Routes { get; set; }
        public DbSet<UserRoute> UserRoutes { get; set; }
        public DbSet<RouteStatus> RouteStatuses { get; set; }
        public DbSet<RelatedRoutes> RelatedRoutes { get; set; }
        public DbSet<UserVertex> UserVertexes { get; set; }
        public DbSet<Vertex> Vetexes { get; set; }
        public DbSet<VertexStatus> VertexStatuses { get; set; }
        public DbSet<VertexConnection> VertexConnections { get; set; }
    }
}
