using BankAccountServiceAPI.Common;
using BankAccountServiceAPI.Features.TransferOperations.MakeTransfer;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace BankAccountServiceAPI.Features.TransferOperations
{
    /// <summary>
    /// Класс контроллера переводов
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TransferController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        // ReSharper disable once ConvertToPrimaryConstructor Считаю обычный конструктор более читаемым
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
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost]
        public async Task<ActionResult<ShowTransactionDto>> MakeTransfer(MakeTransferCommand command)
        {
            // ReSharper disable once SuggestVarOrType_Elsewhere Считаю явную типизацию более понятной для читемости
            MbResult<ShowTransactionDto> result = await _mediator.Send(command);
            return HandleResult(result);
        }
    }
}
