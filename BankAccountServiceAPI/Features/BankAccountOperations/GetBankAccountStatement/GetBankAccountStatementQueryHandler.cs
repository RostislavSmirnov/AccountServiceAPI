using AutoMapper;
using BankAccountServiceAPI.Common;
using BankAccountServiceAPI.Entities;
using BankAccountServiceAPI.Features.TransferOperations;
using BankAccountServiceAPI.Infrastructure.MockRepository;
using MediatR;

namespace BankAccountServiceAPI.Features.BankAccountOperations.GetBankAccountStatement
{
    public class GetBankAccountStatementQueryHandler : IRequestHandler<GetBankAccountStatementQuery, MbResult<BankAccountStatement>>
    {
        private readonly IMockBankAccountRepository _mockBankAccountRepository;
        private readonly IMapper _mapper;

        public GetBankAccountStatementQueryHandler(IMockBankAccountRepository mockBankAccountRepository, IMapper mapper)
        {
            _mockBankAccountRepository = mockBankAccountRepository;
            _mapper = mapper;
        }

        public async Task<MbResult<BankAccountStatement>> Handle(GetBankAccountStatementQuery request, CancellationToken cancellationToken)
        {
            BankAccount? account = await _mockBankAccountRepository.GetBankAccountById(request.AccountId);

            if (account == null)
            {
                throw new Exception($"Счет с ID {request.AccountId} не найден.");
            }

            List<Transaction> transactionsInPeriod = account.Transactions
                .Where(t => t.CreatedDate >= request.StartDate && t.CreatedDate <= request.EndDate)
                .OrderByDescending(t => t.CreatedDate) // Сортировка от новых к старым
                .ToList();

            BankAccountStatement statement = new BankAccountStatement
            {
                AccountId = account.Id,
                OwnerId = account.OwnerId,
                Currency = account.CurrencyCodeISO,
                StatementFrom = account.OpenDate,
                StatementTo = account.CloseDate,
                CurrentBalance = account.Balance,
                Transactions = _mapper.Map<List<ShowTransactionDto>>(transactionsInPeriod)
            };

            return MbResult<BankAccountStatement>.Success(statement);
        }
    }
}
