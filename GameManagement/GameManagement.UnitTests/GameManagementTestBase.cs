using GameManagement.Data;
using GameManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace GameManagement.UnitTests
{
    public class GameManagementTestBase
    {
        protected GameManagementTestBase(DbContextOptions<GameManagementContext> contextOptions)
        {
            ContextOptions = contextOptions;

            Seed();
        }

        protected DbContextOptions<GameManagementContext> ContextOptions { get; }

        private void Seed()
        {
            using (var context = new GameManagementContext(ContextOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                
                context.AddRange(seedPlatforms);
                context.AddRange(seedGames);

                context.SaveChanges();
            }
        }

        public object[] seedPlatforms = {
            new Platform
            {
                Name = "XXX"
            },
            new Platform
            {
                Name = "YYY"
            },
            new Platform
            {
                Name = "ZZZ"
            }
        };

        public object[] seedGames = {
            new Game
            {
                Title = "AAA"
            },
            new Game
            {
                Title = "BBB"
            },
            new Game
            {
                Title = "CCC"
            }
        };
    }
}
