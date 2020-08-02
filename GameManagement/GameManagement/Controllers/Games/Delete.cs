using System.Threading;
using System.Threading.Tasks;
using GameManagement.Data;
using MediatR;

namespace GameManagement.Controllers.Games
{
    public class Delete
    {
        public class Command : IRequest<Response>
        {
            public int Id { get;set; }
        }

        public class Response
        {
            public bool Success { get; set; }
            public string Error { get; set; }
        }

        public class CommandHandler : IRequestHandler<Command, Response>
        {

            private readonly GameManagementContext _context;

            public CommandHandler(GameManagementContext context)
            {
                _context = context;
            }

            public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                return new Response{Success = true};
            }
        }
    }
}