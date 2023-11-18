using CodeRoute.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace CodeRoute.DAL
{
    public class Context : DbContext, IContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
            if ((this.GetService<IDatabaseCreator>() as RelationalDatabaseCreator).Exists() == false)
            {
                ResetTestDB();
            }
        }

        private void ResetTestDB()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();

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
