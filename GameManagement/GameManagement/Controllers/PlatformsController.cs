using System.Threading.Tasks;
using GameManagement.Functions.Platforms;
using GameManagement.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GameManagement.Controllers
{
    [Route("api/platforms")]
    public class PlatformsController : Controller
    {
        private readonly IMediator _mediator;

        public PlatformsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("all")]
        public async Task<Platform[]> List([FromQuery] List.Query query)
        {
            var response = await _mediator.Send(query);
            return response;
        }
    }
}