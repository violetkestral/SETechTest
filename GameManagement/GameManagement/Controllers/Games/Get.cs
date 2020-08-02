using System.Threading;
using System.Threading.Tasks;
using GameManagement.Data;
using GameManagement.Models;
using MediatR;

namespace GameManagement.Controllers.Games
{
    public class Get
    {
        public class Query : IRequest<Game>
        {
            public int Id { get; set; }
        }

        public class QueryHandler : IRequestHandler<Query, Game>
        {

            private readonly GameManagementContext _context;

            public QueryHandler(GameManagementContext context)
            {
                _context = context;
            }

            public async Task<Game> Handle(Query request, CancellationToken cancellationToken)
            {
                return new Game { Id = 1, Title = "FF", Platforms = new[] { new Platform { Name = "PC" } }} ;
            }
        }
    }
}