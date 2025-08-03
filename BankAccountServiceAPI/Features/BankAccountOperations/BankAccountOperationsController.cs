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


        [ProducesResponseType(typeof(MbResult), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("BankAccounts/{Id:guid}")]
        public async Task<IActionResult> DeleteBankAccount(Guid Id)
        {
            var command = new DeleteBankAccountCommand { Id = Id };
            var result = await _mediator.Send(command);
            return HandleResult(result);
        }


        [ProducesResponseType(typeof(BankAccountDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("BankAccounts/{Id:guid}")]
        public async Task<ActionResult<BankAccountDto>> EditBankAccount(Guid Id, EditBankAccountCommand command)
        {
            command.Id = Id;
            MbResult<BankAccountDto> result = await _mediator.Send(command);
            return HandleResult(result);
        }


        [ProducesResponseType(typeof(IEnumerable<BankAccountDto>), StatusCodes.Status200OK)]
        [HttpGet("BankAccounts")]
        public async Task<ActionResult<IEnumerable<BankAccountDto>>> GetAllAccounts()
        {
            var query = new GetBankAccountsQuery();
            var result = await _mediator.Send(query);
            return HandleResult(result);
        }


        [ProducesResponseType(typeof(ActionResult<BankAccountDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("BankAccounts/{Id:guid}")]
        public async Task<ActionResult<BankAccountDto>> GetAccountsById(Guid Id)
        {
            GetBankAccountQuery query = new GetBankAccountQuery{Id = Id};
            MbResult<BankAccountDto> result = await _mediator.Send(query);
            return HandleResult(result);
        }


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
