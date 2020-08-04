using System.Threading;
using System.Threading.Tasks;
using GameManagement.Data;
using GameManagement.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameManagement.Functions.Platforms
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
                return await _context.Platforms.ToArrayAsync(cancellationToken);
            }
        }
    }
}