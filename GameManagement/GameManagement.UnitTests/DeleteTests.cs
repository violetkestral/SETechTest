using AutoFixture;
using AutoFixture.AutoMoq;
using GameManagement.Data;
using GameManagement.Functions.Games;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace GameManagement.UnitTests
{
    public class DeleteTests : GameManagementTestBase
    {
        public DeleteTests()
            : base(
                new DbContextOptionsBuilder<GameManagementContext>()
                    .UseSqlite("Filename=DeleteTest.db")
                    .Options)
        {
        }

        [Fact]
        public void DeleteGame()
        {
            var fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization());

            var gameId = 1;

            //Arrange
            using (var context = new GameManagementContext(ContextOptions))
            {
                fixture.Inject(context);

                var command = new Delete.Command
                {
                    Id = gameId
                };
                var handler = fixture.Create<Delete.CommandHandler>();

                //Act
                var response = handler.Handle(command, new System.Threading.CancellationToken()).Result;

                var game = context.Games.Find(gameId);

                //Assert
                Assert.True(response.Success);
                Assert.Null(game);
            }
        }
    }
}
