using BoxOffice.Model.MovieTicket;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Threading.Tasks;

namespace BoxOffice.Api.Controllers
{
    [ApiController]
    //[Route("api/[controller]")]
    [Route("api/v1/movietickets")]
    public class MovieTicketController : ControllerBase
    {
        private readonly IMovieTicketOrchestrator _movieTicketOrchestrator;
        public MovieTicketController(IMovieTicketOrchestrator movieTicketOrchestrator)
        {
            _movieTicketOrchestrator = movieTicketOrchestrator;
        }

        [HttpPost("{movieId}/tickets/{ticketId}")]
        [SwaggerOperation(
            Summary = "Create movieTicket chunk",
            Description = "Create movieTicket and return created movieTicket chunk",
            OperationId = "PostMovieTicket"
        )]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(400, "Bad request")]
        public async Task<IActionResult> PostAsync(Guid movieId, int ticketId)
        {
            var model = await _movieTicketOrchestrator.CreateAsync(movieId, ticketId);
            return Ok(model);
        }

        [HttpGet("{movieId}/tickets")]
        [SwaggerOperation(
            Summary = "Get movieTicket chunk by movieID",
            Description = "Return movieTicket chunk by movieID",
            OperationId = "GetMovieTicketByMovieId"
        )]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(400, "Bad request")]
        public async Task<IActionResult> GetTicketsByMovieId(Guid movieId)
        {
            var model = await _movieTicketOrchestrator.GetTicketsAsync(movieId);
            return Ok(model);
        }
    }
}
