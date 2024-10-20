using MediatR;
using Microsoft.AspNetCore.Mvc;
using TvMaze.Application.Dtos;
using TvMaze.Application.Shows.Queries.GetAllShows;

namespace TvMaze.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ShowsController(ILogger<ShowsController> logger, IMediator mediator) : ControllerBase
{
    private readonly ILogger<ShowsController> _logger = logger ??
        throw new ArgumentNullException(nameof(logger));
    private readonly IMediator _mediator = mediator ??
        throw new ArgumentNullException(nameof(mediator));

    [HttpGet]
    [ProducesResponseType<IEnumerable<ShowDto>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<IList<ShowDto>>> GetAll()
    {
        _logger.LogInformation("Get All Shows invoked");
        var retVal = await _mediator.Send(new GetAllShowsQuery());
        return Ok(retVal);
    }
}
