using AutoMapper;
using BoxOffice.Model.Movie;
using BoxOffice.Orchestrators.Ticket.Contract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Threading.Tasks;

namespace BoxOffice.Api.Controllers
{
    [ApiController]
    //[Route("[controller]")]
    [Route("api/v1/movies")]
    public class MoviesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<MoviesController> _logger;
        private IMovieOrchestrator _movieOrchestrator;

        public MoviesController(
            IMapper mapper,
            ILogger<MoviesController> logger,
            IMovieOrchestrator movieOrchestrator)
        {
            _mapper = mapper;
            _logger = logger;
            _movieOrchestrator = movieOrchestrator;
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Get all movies",          //description of HTTP method
            Description = "Return a list of all movies",  //explanation what method produce (expanded)
            OperationId = "GetMovies"
        //Tags = new[] { "Movies", "Tickets" }
        )]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(400, "Bad request")]
        public async Task<IActionResult> Get()
        {
            var movie = await _movieOrchestrator.GetAllAsync();

            return Ok(movie);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Get movie by ID",
            Description = "Return movie by ID",
            OperationId = "GetMovieById"
        )]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(400, "Bad request")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var movieById = await _movieOrchestrator.GetByIdAsync(id);

            return Ok(movieById);
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Create movie",
            Description = "Create movie and return created movie",
            OperationId = "PostMovie"
        )]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(400, "Bad request")]
        public async Task<IActionResult> PostAsync(CreateMovie movie)
        {
            var model = _mapper.Map<CreateMovie, Movie>(movie);
            var result = await _movieOrchestrator.CreateAsync(model);
            return Ok(result);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Update movie",
            Description = "Update movie and return updated movie",
            OperationId = "PostMovie"
        )]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(400, "Bad request")]
        public async Task<IActionResult> PutAsync(Guid id, EditMovie model)
        {
            Movie modelToUpdate = _mapper.Map<EditMovie, Movie>(model);
            modelToUpdate.Id = id;
            Movie entity = await _movieOrchestrator.UpdateAsync(modelToUpdate);

            return Ok(entity);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Delete movie by ID",
            Description = "Delete movie by ID and return deleted movie",
            OperationId = "PostMovie"
        )]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(400, "Bad request")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var deleteEntity = await _movieOrchestrator.DeleteAsync(id);
            
            return Ok(deleteEntity);
        }

    }
}