using BankAccountServiceAPI.Common;
using BankAccountServiceAPI.Features.TransferOperations.MakeTransfer;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankAccountServiceAPI.Features.TransferOperations
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransferController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public TransferController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Выполнение перевода между счетами
        /// </summary>
        /// <param name="command"> Команда с данными необходимыми чтобы осуществить перевод </param> 
        /// <returns> ActionResult с информацией о транзакции </returns> 
        [ProducesResponseType(typeof(ShowTransactionDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult<ShowTransactionDto>> MakeTransfer(MakeTransferCommand command)
        {
            MbResult<ShowTransactionDto> result = await _mediator.Send(command);
            return HandleResult(result);
        }
    }
}
