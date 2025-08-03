using AutoMapper;
using BankAccountServiceAPI.Common;
using BankAccountServiceAPI.Entities;
using BankAccountServiceAPI.Entities.Enums;
using BankAccountServiceAPI.Infrastructure.CurrenciesSupport;
using BankAccountServiceAPI.Infrastructure.MockRepository;
using MediatR;

namespace BankAccountServiceAPI.Features.TransferOperations.MakeTransfer
{
    public class MakeTransferCommandHandler : IRequestHandler<MakeTransferCommand, MbResult<ShowTransactionDto>>
    {
        private readonly IMapper _mapper;
        private readonly IMockBankAccountRepository _mockBankAccountRepository;
        private readonly ICurrencyService _currencyService;

        public MakeTransferCommandHandler(IMapper mapper, IMockBankAccountRepository mockBankAccountRepository, ICurrencyService currencyService)
        {
            _mapper = mapper;
            _mockBankAccountRepository = mockBankAccountRepository;
            _currencyService = currencyService;
        }

        public async Task<MbResult<ShowTransactionDto>> Handle(MakeTransferCommand request, CancellationToken cancellationToken)
        {
            BankAccount accountOut = await _mockBankAccountRepository.GetBankAccountById(request.AccountId);

            if (accountOut == null)
            {
                throw new Exception($"Аккаунта отправителя с ID {request.AccountId} не существует");
            }

            BankAccount accountIn = await _mockBankAccountRepository.GetBankAccountById(request.CounterPartyAccountId);

            if (accountIn == null)
            {
                throw new Exception($"Аккаунта получателя с ID {request.AccountId} не существует");
            }

            if (accountIn.CurrencyCodeISO != accountOut.CurrencyCodeISO)
            {
                throw new InvalidOperationException("Невозможно перевод средств на счета с разной валютой");
            }
            
            if (!await _currencyService.ThisCurrencyIsSupported(accountOut.CurrencyCodeISO))
            {
                throw new InvalidOperationException($"Валюта '{accountOut.CurrencyCodeISO}' не поддерживается.");
            }

            if (accountOut.Balance < request.Amount)
            {
                throw new InvalidOperationException("недостаточно средств на счёте отправителя");
            }

            accountOut.Balance -= request.Amount; //Уменьшаю баланс с счёта отправителя
            accountIn.Balance += request.Amount; //Увеличиваю баланс счёта получателя

            Transaction outTransaction = new Transaction
            {
                Id = Guid.NewGuid(),
                AccountId = accountOut.Id,
                CounterPartyAccountId = accountIn.Id,
                Amount = request.Amount,
                CurrencyCodeISO = accountOut.CurrencyCodeISO,
                TransactionType = TransactionType.Credit,
                TransactionInOrOut = TransactionInOrOut.Outgoing,
                MetaData = request.MetaData,
                CreatedDate = DateTime.UtcNow
            };

            switch (accountOut.AccountType)
            {
                case AccountType.Checking:
                    outTransaction.TransactionType = TransactionType.Debit;
                    break;

                case AccountType.Credit:
                    outTransaction.TransactionType = TransactionType.Credit;
                    break;

                case AccountType.Deposit:
                    outTransaction.TransactionType = TransactionType.Debit;
                    break;
            }

            Transaction inTransaction = new Transaction
            {
                Id = Guid.NewGuid(),
                AccountId = accountIn.Id,
                CounterPartyAccountId = accountOut.Id,
                Amount = request.Amount,
                CurrencyCodeISO = accountOut.CurrencyCodeISO,
                TransactionType = TransactionType.Debit,
                TransactionInOrOut = TransactionInOrOut.Incoming,
                MetaData = request.MetaData,
                CreatedDate = DateTime.UtcNow
            };

            switch (accountIn.AccountType)
            {
                case AccountType.Checking:
                    inTransaction.TransactionType = TransactionType.Credit;
                    break;

                case AccountType.Credit:
                    inTransaction.TransactionType = TransactionType.Debit;
                    break;

                case AccountType.Deposit:
                    inTransaction.TransactionType = TransactionType.Credit;
                    break;
            }

            accountOut.Transactions.Add(outTransaction);
            accountIn.Transactions.Add(inTransaction);

            await _mockBankAccountRepository.EditBankAccount(accountIn);
            await _mockBankAccountRepository.EditBankAccount(accountOut);

            var result = _mapper.Map<ShowTransactionDto>(outTransaction);
            return MbResult<ShowTransactionDto>.Success(result);
        }
    }
}
