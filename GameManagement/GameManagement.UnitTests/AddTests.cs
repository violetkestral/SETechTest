using System.Linq;
using AutoFixture;
using AutoFixture.AutoMoq;
using GameManagement.Data;
using GameManagement.Functions.Games;
using GameManagement.Models;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace GameManagement.UnitTests
{
    public class AddTests : GameManagementTestBase
    {
        public AddTests()
            : base(
                new DbContextOptionsBuilder<GameManagementContext>()
                    .UseSqlite("Filename=AddTest.db")
                    .Options)
        {
        }
        
        [Fact]
        public void AddNewGame()
        {
            var fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization());

            var newGame = new Game
            {
                Title = "DDD"
            };
            
            //Arrange
            using (var context = new GameManagementContext(ContextOptions))
            {
                fixture.Inject(context);

                var command = new Add.Command {Title = newGame.Title};
                var handler = fixture.Create<Add.CommandHandler>();

                //Act
                var response = handler.Handle(command, new System.Threading.CancellationToken()).Result;

                var game = context.Games.SingleOrDefaultAsync(g => g.Title == newGame.Title).Result;

                //Assert
                Assert.True(response.Success);
                Assert.NotNull(game);
                Assert.Equal(newGame.Title, game.Title);
            }
        }

        [Fact]
        public void AddNewGame_WithPlatform()
        {
            var fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization());

            var newGameTitle = "DDD";
            var newGamePlatform = (Platform)SeedPlatforms[0];

            //Arrange
            using (var context = new GameManagementContext(ContextOptions))
            {
                fixture.Inject(context);

                var command = new Add.Command
                {
                    Title = newGameTitle, 
                    Platforms = new []
                    {
                        newGamePlatform
                    }
                };
                var handler = fixture.Create<Add.CommandHandler>();

                //Act
                var response = handler.Handle(command, new System.Threading.CancellationToken()).Result;

                var game = context.Games
                    .Include(g => g.GamePlatforms)
                    .ThenInclude(p => p.Platform)
                    .SingleOrDefaultAsync(g => g.Title == newGameTitle).Result;

                //Assert
                Assert.True(response.Success);
                Assert.NotNull(game);
                Assert.Equal(newGameTitle, game.Title);
                Assert.Equal(newGamePlatform.Id, game.GamePlatforms.FirstOrDefault()?.Platform.Id);
                Assert.Equal(newGamePlatform.Name, game.GamePlatforms.FirstOrDefault()?.Platform.Name);
            }
        }
    }
}
