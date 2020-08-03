using System.Linq;
using AutoFixture;
using AutoFixture.AutoMoq;
using GameManagement.Controllers.Games;
using GameManagement.Data;
using GameManagement.Models;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace GameManagement.UnitTests
{
    public class EditTests : GameManagementTestBase
    {
        public EditTests()
            : base(
                new DbContextOptionsBuilder<GameManagementContext>()
                    .UseSqlite("Filename=EditTest.db")
                    .Options)
        {
        }

        [Fact]
        public void EditGame_ChangeTitle()
        {
            var fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization());

            var gameId = 1;
            var newTitle = "111";

            //Arrange
            using (var context = new GameManagementContext(ContextOptions))
            {
                fixture.Inject(context);

                var command = new Edit.Command
                {
                    Id = gameId,
                    Title = newTitle
                };
                var handler = fixture.Create<Edit.CommandHandler>();

                var oldTitle = context.Games.Find(gameId).Title;

                //Act
                var response = handler.Handle(command, new System.Threading.CancellationToken()).Result;

                var game = context.Games.Find(gameId);

                //Assert
                Assert.True(response.Success);
                Assert.NotNull(game);
                Assert.Equal(newTitle, game.Title);
                Assert.NotEqual(newTitle, oldTitle);
            }
        }
        
        [Fact]
        public void EditGame_AddPlatform()
        {
            var fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization());

            var gameId = 1;
            var gameTitle = ((Game)seedGames[0]).Title;
            var platforms = new[]
            {
                (Platform)seedPlatforms[0]
            };

            //Arrange
            using (var context = new GameManagementContext(ContextOptions))
            {
                fixture.Inject(context);

                var command = new Edit.Command
                {
                    Id = gameId,
                    Title = gameTitle,
                    Platforms = platforms
                };
                var handler = fixture.Create<Edit.CommandHandler>();

                //Act
                var response = handler.Handle(command, new System.Threading.CancellationToken()).Result;

                var game = context.Games
                    .Include(g => g.GamePlatforms)
                    .ThenInclude(p => p.Platform)
                    .SingleOrDefaultAsync(g => g.Id == gameId).Result; ;

                //Assert
                Assert.True(response.Success);
                Assert.NotNull(game);
                Assert.Equal(gameTitle, game.Title);
                Assert.Equal(platforms[0].Id, game.GamePlatforms.FirstOrDefault()?.Platform.Id);
                Assert.Equal(platforms[0].Name, game.GamePlatforms.FirstOrDefault()?.Platform.Name);
            }
        }
    }
}
