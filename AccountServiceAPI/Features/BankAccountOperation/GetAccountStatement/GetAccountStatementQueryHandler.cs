using AccountServiceAPI.Features.BankAccountModels;
using AccountServiceAPI.Features.BankAccountOperation.GetAccountStatement;
using AccountServiceAPI.Infrastructure.Persistence;
using AutoMapper;
using MediatR;

namespace AccountService.Features.Accounts.GetAccountStatement
{
    public class GetAccountStatementQueryHandler : IRequestHandler<GetAccountStatementQuery, AccountStatementDto>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public GetAccountStatementQueryHandler(IAccountRepository accountRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        public async Task<AccountStatementDto> Handle(GetAccountStatementQuery request, CancellationToken cancellationToken)
        {
            var account = await _accountRepository.GetAccountByIdAsync(request.AccountId);

            if (account == null)
            {
                throw new KeyNotFoundException($"Счет с ID {request.AccountId} не найден.");
            }

            var transactionsInPeriod = account.Transactions
                .Where(t => t.TransactionDate >= request.FromDate && t.TransactionDate <= request.ToDate)
                .OrderByDescending(t => t.TransactionDate) // Сортируем от новых к старым
                .ToList();

            var statementDto = new AccountStatementDto
            {
                AccountId = account.Id,
                OwnerId = account.OwnerId,
                CurrentBalance = account.Balance,
                Currency = account.Currency,
                StatementFrom = request.FromDate,
                StatementTo = request.ToDate,
                Transactions = _mapper.Map<List<TransactionDto>>(transactionsInPeriod)
            };
            return statementDto;
        }
    }
}