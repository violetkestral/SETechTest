using System;
using System.Threading;
using System.Threading.Tasks;
using GameManagement.Data;
using MediatR;

namespace GameManagement.Functions.Games
{
    public class Delete
    {
        public class Command : IRequest<Response>
        {
            public int Id { get; set; }
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
                var success = false;
                var error = "";

                try
                {
                    var game = await _context.Games.FindAsync(request.Id);
                    _context.Games.Remove(game);
                    await _context.SaveChangesAsync(cancellationToken);
                    success = true;
                }
                catch (Exception e)
                {
                    error = e.Message;
                }

                return new Response
                {
                    Success = success,
                    Error = error
                };
            }
        }
    }
}