using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace NovelistsApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class ApiControllerBase : ControllerBase
{
    private readonly ILogger _logger;
    protected readonly IMediator _mediator;

    protected ApiControllerBase(ILogger logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }
}
