using AccountServiceAPI.Features.BankAccountModels;
using AccountServiceAPI.Features.BankAccountOperation.CreateBankAccount;
using AccountServiceAPI.Features.BankAccountOperation.DeleteBankAccount;
using AccountServiceAPI.Features.BankAccountOperation.GetAccountStatement;
using AccountServiceAPI.Features.BankAccountOperation.GetBankAccountById;
using AccountServiceAPI.Features.BankAccountOperation.GetBankAccountList;
using AccountServiceAPI.Features.Transactions.RegisterTransaction;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AccountServiceAPI.Features.BankAccountOperation
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("Accounts")]
        [ProducesResponseType(typeof(IEnumerable<BankAccountDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAccounts()
        {
            var query = new GetAccountsFromQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(BankAccountDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAccount(Guid id)
        {
            var query = new GetAccountByIdQuery { Id = id };

            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpPost("CreateAccount")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAccount([FromBody] CreateBankAccountCommand accountData)
        {
            var accountId = await _mediator.Send(accountData);
            return CreatedAtAction(nameof(GetAccount), new { id = accountId }, new { id = accountId });
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAccount(Guid id)
        {
            var command = new DeleteAccountByIdCommand { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpPost("{id:guid}/transactions")]
        public async Task<IActionResult> RegisterTransaction(Guid id, [FromBody] RegisterTransactionCommand command)
        {
            command.AccountId = id;
            var transactionId = await _mediator.Send(command);
            return Ok(new { transactionId });
        }

        [HttpGet("{id:guid}/statement")]
        [ProducesResponseType(typeof(AccountStatementDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAccountStatement(Guid id, [FromQuery] DateTime fromDate, [FromQuery] DateTime toDate)
        {
            var query = new GetAccountStatementQuery
            {
                AccountId = id,
                FromDate = fromDate,
                ToDate = toDate
            };

            var statement = await _mediator.Send(query);

            return Ok(statement);
        }
    }
}