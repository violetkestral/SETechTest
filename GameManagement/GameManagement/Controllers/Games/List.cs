using System.Threading;
using System.Threading.Tasks;
using GameManagement.Data;
using GameManagement.Models;
using MediatR;

namespace GameManagement.Controllers.Games
{
    public class List
    {
        public class Query : IRequest<Game[]>
        {
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
                return new []
                    {
                        new Game{Id=1,Title="FF", Platforms = new []{new Platform{Name="PC"}}},
                        new Game{Id=2,Title="KH", Platforms = new []{new Platform{Name="PS4"}}},
                        new Game{Id=3,Title="XYZ", Platforms = new []{new Platform{Name="PC"},new Platform{Name="PS4"}}}
                };
            }
        }
    }
}
