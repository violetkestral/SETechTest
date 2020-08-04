using AutoFixture;
using AutoFixture.AutoMoq;
using GameManagement.Data;
using GameManagement.Functions.Games;
using GameManagement.Models;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace GameManagement.UnitTests
{
    public class ListTests : GameManagementTestBase
    {
        public ListTests()
            : base(
                new DbContextOptionsBuilder<GameManagementContext>()
                    .UseSqlite("Filename=ListTest.db")
                    .Options)
        {
        }
        
        [Fact]
        public void ListAllGames()
        {
            var fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization());

            //Arrange
            using (var context = new GameManagementContext(ContextOptions))
            {
                fixture.Inject(context);

                var command = new List.Query();
                var handler = fixture.Create<List.QueryHandler>();

                //Act
                var games = handler.Handle(command, new System.Threading.CancellationToken()).Result;

                //Assert
                Assert.Equal(SeedGames.Length,games.Length);
                Assert.Equal(((Game)SeedGames[0]).Title,games[0].Title);
            }
        }
    }
}
