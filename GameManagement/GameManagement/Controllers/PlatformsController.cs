using System.Threading.Tasks;
using GameManagement.Controllers.Platforms;
using GameManagement.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GameManagement.Controllers
{

    [Route("api/platforms")]
    public class PlatformsController : Controller
    {
        public PlatformsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        private readonly IMediator _mediator;

        [HttpGet("all")]
        public async Task<Platform[]> List([FromQuery] List.Query query)
        {
            var response = await _mediator.Send(query);
            return response;
        }
    }
}