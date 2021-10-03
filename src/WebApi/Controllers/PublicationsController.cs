using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NovelistsApi.Infrastructure.Features.Publications;

namespace NovelistsApi.Controllers;

public class PublicationsController : ApiControllerBase
{
    public PublicationsController(ILogger<PublicationsController> logger, IMediator mediator) : base(logger, mediator)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetPublications(CancellationToken cancellationToken)
    {
        var query = new GetAll.Query();
        var response = await _mediator.Send(query, cancellationToken);

        if (response is null)
        {
            return NotFound(response);
        }

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPublication(Guid id, CancellationToken cancellationToken)
    {
        var query = new Get.Query(id);
        var response = await _mediator.Send(query, cancellationToken);

        if (response is null)
        {
            return NotFound(response);
        }

        return Ok(response);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> PutPublication(Guid id, [FromBody] Update.Envelope envelope, CancellationToken cancellationToken)
    {
        var command = new Update.Command(id, envelope);
        var response = await _mediator.Send(command, cancellationToken);

        if (response is null)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> PostPublication([FromBody] Create.Command command, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(command, cancellationToken);

        if (response is null)
        {
            return BadRequest(response);
        }

        return CreatedAtAction(nameof(GetPublication), new { response.Id }, response);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeletePublication(Guid id, CancellationToken cancellationToken)
    {
        var command = new Delete.Command(id);
        var response = await _mediator.Send(command, cancellationToken);

        if (response is null)
        {
            return NotFound(response);
        }

        return Ok(response);
    }
}
