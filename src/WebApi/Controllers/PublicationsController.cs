using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NovelistsApi.Infrastructure.Features.Publications;

namespace NovelistsApi.Controllers
{
    public class PublicationsController : ApiControllerBase
    {
        public PublicationsController(ILogger<PublicationsController> logger, IMediator mediator) : base(logger, mediator)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] GetAll.Query query, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(query, cancellationToken);

            if (response is null)
            {
                return NotFound(response);
            }

            return Ok(response);
        }
    }
}
