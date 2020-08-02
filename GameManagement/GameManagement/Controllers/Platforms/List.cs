using System.Threading;
using System.Threading.Tasks;
using GameManagement.Data;
using GameManagement.Models;
using MediatR;

namespace GameManagement.Controllers.Platforms
{
    public class List
    {
        public class Query : IRequest<Platform[]>
        {
        }

        public class QueryHandler : IRequestHandler<Query, Platform[]>
        {
            private readonly GameManagementContext _context;

            public QueryHandler(GameManagementContext context)
            {
                _context = context;
            }

            public async Task<Platform[]> Handle(Query request, CancellationToken cancellationToken)
            {
                return new[]
                {
                    new Platform { Name = "PC" }, 
                    new Platform { Name = "PS4" },
                    new Platform { Name = "Nintendo Switch" }
                };
            }
        }
    }
}
