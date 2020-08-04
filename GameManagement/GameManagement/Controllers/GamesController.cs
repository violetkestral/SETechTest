using System.Threading.Tasks;
using GameManagement.Functions.Games;
using GameManagement.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GameManagement.Controllers
{
    [Route("api/games")]
    public class GamesController : Controller
    {
        private readonly IMediator _mediator;

        public GamesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("all")]
        public async Task<Game[]> List([FromQuery] List.Query query)
        {
            var response = await _mediator.Send(query);
            return response;
        }

        [HttpGet("game/{id:int}")]
        public async Task<Game> GetGame([FromRoute] Get.Query query)
        {
            var response = await _mediator.Send(query);
            return response;
        }

        [HttpPost("add")]
        public async Task<Add.Response> Add([FromBody] Add.Command command)
        {
            var response = await _mediator.Send(command);
            return response;
        }

        [HttpPost("edit")]
        public async Task<Edit.Response> Edit([FromBody] Edit.Command command)
        {
            var response = await _mediator.Send(command);
            return response;
        }

        [HttpDelete("delete/{id:int}")]
        public async Task<Delete.Response> Delete([FromRoute] Delete.Command command)
        {
            var response = await _mediator.Send(command);
            return response;
        }
    }
}