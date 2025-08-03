using BankAccountServiceAPI.Common;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BankAccountServiceAPI.Features.BankAccountOperations;
using BankAccountServiceAPI.Features.BankAccountOperations.CreateBankAccount;
using BankAccountServiceAPI.Features.BankAccountOperations.DeleteBankAccount;
using BankAccountServiceAPI.Features.BankAccountOperations.EditBankAccount;
using BankAccountServiceAPI.Features.BankAccountOperations.GetBankAccountById;
using BankAccountServiceAPI.Features.BankAccountOperations.GetBankAccounts;
using BankAccountServiceAPI.Features.BankAccountOperations.GetBankAccountStatement;

namespace BankAccountServiceAPI.Features.BankAccountOperations
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankAccountOperationsController : ApiControllerBase
    {
        private readonly IMediator _mediator;
        public  BankAccountOperationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Создает новый банковский счет.
        /// </summary>
        /// <param name="command">Команда с данными для создания счета (ID владельца, тип счета, валюта и т.д.).</param>
        /// <returns>IActionResult с ID созданного счета и статусом HTTP 201 или HTTP 400 при неверном запросе.</returns>
        [HttpPost("BankAccounts")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateBankAccount(CreateBankAccountCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.IsSuccess)
            {
                 return CreatedAtAction(nameof(GetAccountsById), new { id = result.Value }, new { id = result.Value });
            }

            return BadRequest(result.Errors);
        }


        /// <summary>
        /// Удаляет банковский счет по его ID.
        /// </summary>
        /// <param name="Id">Уникальный идентификатор счета для удаления.</param>
        /// <returns>IActionResult со статусом HTTP 204 при успехе или HTTP 404, если счет не найден.</returns>
        [ProducesResponseType(typeof(MbResult), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("BankAccounts/{Id:guid}")]
        public async Task<IActionResult> DeleteBankAccount(Guid Id)
        {
            var command = new DeleteBankAccountCommand { Id = Id };
            var result = await _mediator.Send(command);
            return HandleResult(result);
        }


        /// <summary>
        /// Обновляет существующий банковский счет.
        /// </summary>
        /// <param name="Id">Уникальный идентификатор счета для обновления.</param>
        /// <param name="command">Команда с обновленными данными счета (валюта, процентная ставка и т.д.).</param>
        /// <returns>ActionResult с обновленными данными счета и статусом HTTP 200, или HTTP 404, если счет не найден, или HTTP 400 при неверном запросе.</returns>
        [ProducesResponseType(typeof(BankAccountDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("BankAccounts/{Id:guid}")]
        public async Task<ActionResult<BankAccountDto>> EditBankAccount(Guid Id, EditBankAccountCommand command)
        {
            command.Id = Id;
            MbResult<BankAccountDto> result = await _mediator.Send(command);
            return HandleResult(result);
        }


        /// <summary>
        /// Получает список всех банковских счетов.
        /// </summary>
        /// <returns>ActionResult со списком счетов и статусом HTTP 200.</returns>
        [ProducesResponseType(typeof(IEnumerable<BankAccountDto>), StatusCodes.Status200OK)]
        [HttpGet("BankAccounts")]
        public async Task<ActionResult<IEnumerable<BankAccountDto>>> GetAllAccounts()
        {
            var query = new GetBankAccountsQuery();
            var result = await _mediator.Send(query);
            return HandleResult(result);
        }


        /// <summary>
        /// Получает банковский счет по его ID.
        /// </summary>
        /// <param name="Id">Уникальный идентификатор счета.</param>
        /// <returns>ActionResult с данными счета и статусом HTTP 200, или HTTP 404, если счет не найден.</returns>
        [ProducesResponseType(typeof(ActionResult<BankAccountDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("BankAccounts/{Id:guid}")]
        public async Task<ActionResult<BankAccountDto>> GetAccountsById(Guid Id)
        {
            GetBankAccountQuery query = new GetBankAccountQuery{Id = Id};
            MbResult<BankAccountDto> result = await _mediator.Send(query);
            return HandleResult(result);
        }

        /// <summary>
        /// Получает выписку по банковскому счету за указанный период.
        /// </summary>
        /// <param name="id">Уникальный идентификатор счета.</param>
        /// <param name="startDate">Дата начала периода выписки.</param>
        /// <param name="endDate">Дата окончания периода выписки.</param>
        /// <returns>IActionResult с выпиской по счету и статусом HTTP 200, или HTTP 404, если счет не найден.</returns>
        [ProducesResponseType(typeof(BankAccountStatement), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id:guid}/Statement")]
        public async Task<IActionResult> GetBankAccountStatement(Guid id, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            GetBankAccountStatementQuery query = new GetBankAccountStatementQuery()
            {
                AccountId = id,
                StartDate = startDate,
                EndDate = endDate
            };
            var result = await _mediator.Send(query);
            return HandleResult(result);
        }
    }
}
