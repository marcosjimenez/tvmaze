using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TvMaze.Application.Shows.Commands.ImportShows;

namespace TvMaze.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ImportController(ILogger<ShowsController> logger, IMediator mediator) : ControllerBase
{
    private readonly ILogger<ShowsController> _logger = logger ??
        throw new ArgumentNullException(nameof(logger));
    private readonly IMediator _mediator = mediator ??
        throw new ArgumentNullException(nameof(mediator));

    [HttpPost("shows")]
    [Authorize(Policy = "ApiKeyPolicy")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> ImportShows()
    {
        _logger.LogInformation("Import Shows invoked");
        await _mediator.Send(new ImportShowsCommand());
        return Ok();
    }

}
