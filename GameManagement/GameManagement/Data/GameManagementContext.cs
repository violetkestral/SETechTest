using Microsoft.EntityFrameworkCore;
using GameManagement.Models;

namespace GameManagement.Data
{
    public class GameManagementContext : DbContext
    {
        public GameManagementContext(DbContextOptions<GameManagementContext> options)
            : base(options)
        {
        }

        public DbSet<Game> Games { get; set; }
        public DbSet<Platform> Platforms { get; set; }
        public DbSet<GamePlatform> GamePlatforms { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Platform>().HasData(
                new Platform { Id = 1, Name = "PC" },
                new Platform { Id = 2, Name = "PS4" },
                new Platform { Id = 3, Name = "Nintendo Switch" }
                );
        }
    }
}
