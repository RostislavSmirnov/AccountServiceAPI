using AccountServiceAPI.Features.Transfers.MakeTransfer;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AccountServiceAPI.Features.Transfers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransfersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TransfersController(IMediator mediator)
        { _mediator = mediator; }

        [HttpPost]
        public async Task<IActionResult> MakeTransfer([FromBody] MakeTransferCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }
    }
}