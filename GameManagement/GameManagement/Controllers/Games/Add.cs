using System;
using System.Threading;
using System.Threading.Tasks;
using GameManagement.Data;
using GameManagement.Models;
using MediatR;

namespace GameManagement.Controllers.Games
{
    public class Add
    {
        public class Command : IRequest<Response>
        {
            public string Title { get; set; }
            public Platform[] Platforms { get; set; }
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
                    var game = new Game
                    {
                        Title = request.Title
                    };
                    await _context.Games.AddAsync(game, cancellationToken);

                    if (request.Platforms != null)
                    {
                        foreach (var platform in request.Platforms)
                        {
                            var gamePlatform = new GamePlatform
                            {
                                Game = game,
                                PlatformId = platform.Id
                            };
                            await _context.GamePlatforms.AddAsync(gamePlatform, cancellationToken);
                        }
                    }

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