using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GameManagement.Data;
using GameManagement.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameManagement.Controllers.Games
{
    public class List
    {
        public class Query : IRequest<Game[]>
        {
        }
        public class Response
        {
            public ICollection<Game> Games;

            public class Game
            {
                public int Id { get; set; }

                public string Title { get; set; }
                public ICollection<Platform> Platforms { get; set; }
            }

            public class Platform
            {
                public int Id { get; set; }
                public string Name { get; set; }
            }
        }

        public class QueryHandler : IRequestHandler<Query, Game[]>
        {
            private readonly GameManagementContext _context;

            public QueryHandler(GameManagementContext context)
            {
                _context = context;
            }

            public async Task<Game[]> Handle(Query request, CancellationToken cancellationToken)
            {
                return _context.Games
                    .Include(g => g.GamePlatforms)
                    .ThenInclude(p => p.Platform)
                    .ToArray();
            }
        }
    }
}
