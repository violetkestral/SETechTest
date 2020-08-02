using System.Threading;
using System.Threading.Tasks;
using GameManagement.Data;
using GameManagement.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
                return await _context.Games
                    .Include(g => g.GamePlatforms)
                    .ThenInclude(p => p.Platform)
                    .SingleAsync(g => g.Id == request.Id, cancellationToken: cancellationToken);
            }
        }
    }
}