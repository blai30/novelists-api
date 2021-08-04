using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NovelistsApi.Infrastructure.Features.Users;

namespace NovelistsApi.Controllers
{
    public class UsersController : ApiControllerBase
    {
        public UsersController(ILogger<UsersController> logger, IMediator mediator) : base(logger, mediator)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers(GetAll.Query query, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(query, cancellationToken);

            if (response is null)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(Guid id, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new Get.Query(id), cancellationToken);

            if (response is null)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(Guid id, [FromBody] Update.Model model, CancellationToken cancellationToken)
        {
            var command = new Update.Command(id, model);
            var response = await _mediator.Send(command, cancellationToken);

            if (response is null)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> PostUser([FromBody] Create.Command command, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(command, cancellationToken);

            if (response is null)
            {
                return BadRequest(response);
            }

            return CreatedAtAction(nameof(GetUser), response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new Delete.Command(id), cancellationToken);

            if (response is null)
            {
                return NotFound(response);
            }

            return Ok(response);
        }
    }
}
