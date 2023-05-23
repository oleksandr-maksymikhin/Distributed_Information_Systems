using AutoMapper;
using BoxOffice.Model.Ticket;
using BoxOffice.Orchestrators.Ticket.Contract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoxOffice.Api.Controllers
{
    [ApiController]
    //[Route("[controller]")]
    [Route("api/v1/tickets")]
    public class TicketsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<TicketsController> _logger;
        private ITicketOrchestrator _ticketOrchestrator;

        public TicketsController(
            IMapper mapper,
            ILogger<TicketsController> logger,
            ITicketOrchestrator ticketOrchestrator)
        {
            _mapper = mapper;
            _logger = logger;
            _ticketOrchestrator = ticketOrchestrator;
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Get all tickets",          //description of HTTP method
            Description = "Return a list of all tickets",  //explanation what method produce (expanded)
            OperationId = "GetTickets"
        //Tags = new[] { "Movies", "Tickets" }
        )]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(400, "Bad request")]
        public async Task<IActionResult> Get()
        {
            var ticket = await _ticketOrchestrator.GetAllAsync();

            return Ok(ticket);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Get ticket by ID",
            Description = "Return ticket by ID",
            OperationId = "GetTicketById"
        )]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(400, "Bad request")]
        public async Task<IActionResult> GetById(int id)
        {
            var ticketById = await _ticketOrchestrator.GetByIdAsync(id);

            return Ok(ticketById);
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Create ticket",
            Description = "Create ticket and return created ticket",
            OperationId = "PostTicket"
        )]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(400, "Bad request")]
        public async Task<IActionResult> PostAsync(CreateTicket ticket)
        {
            var model = _mapper.Map<CreateTicket, Ticket>(ticket);
            var result = await _ticketOrchestrator.CreateAsync(model);
            return Ok(result);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Update ticket",
            Description = "Update ticket and return updated ticket",
            OperationId = "PostTicket"
        )]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(400, "Bad request")]
        public async Task<IActionResult> PutAsync(int id, EditTicket model)
        {
            var modelToUpdate = _mapper.Map<EditTicket, Ticket>(model);
            //modelToUpdate.Id = id;
            Ticket entity = await _ticketOrchestrator.UpdateAsync(id, modelToUpdate);

            return Ok(entity);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Delete ticket by ID",
            Description = "Delete ticket by ID and return deleted ticket",
            OperationId = "PostTicket"
        )]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(400, "Bad request")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var deleteEntity = await _ticketOrchestrator.DeleteAsync(id);
            
            return Ok(deleteEntity);
        }

    }
}