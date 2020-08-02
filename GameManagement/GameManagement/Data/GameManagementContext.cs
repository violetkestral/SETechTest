using Microsoft.EntityFrameworkCore;
using GameManagement.Models;

namespace GameManagement.Data
{
    public class GameManagementContext : DbContext
    {
        public GameManagementContext (DbContextOptions<GameManagementContext> options)
            : base(options)
        {
        }

        public DbSet<Game> Games { get; set; }
        public DbSet<Platform> Platforms { get; set; }
    }
}
