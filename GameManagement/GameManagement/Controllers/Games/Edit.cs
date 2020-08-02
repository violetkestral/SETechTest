using System;
using System.Threading;
using System.Threading.Tasks;
using GameManagement.Data;
using GameManagement.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameManagement.Controllers.Games
{
    public class Edit
    {
        public class Command : IRequest<Response>
        {
            public int Id { get; set; }
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
                    var game = await _context.Games
                        .Include(g => g.GamePlatforms)
                        .SingleAsync(g => g.Id == request.Id, cancellationToken: cancellationToken);

                    game.Title = request.Title;
                    game.GamePlatforms.Clear();

                    foreach (var platform in request.Platforms)
                    {
                        var gamePlatform = new GamePlatform
                        {
                            Game = game,
                            PlatformId = platform.Id
                        };
                        await _context.GamePlatforms.AddAsync(gamePlatform, cancellationToken);
                    }

                    _context.Games.Update(game);

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