using AccountServiceAPI.Entities;
using AccountServiceAPI.Entities.Enums;
using AccountServiceAPI.Infrastructure.Persistence;
using MediatR;

namespace AccountServiceAPI.Features.Transfers.MakeTransfer
{
    public class MakeTransferCommandHandler : IRequestHandler<MakeTransferCommand>
    {
        private readonly IAccountRepository _accountRepository;

        public MakeTransferCommandHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }


        public async Task Handle(MakeTransferCommand request, CancellationToken cancellationToken)
        {
            var fromAccount = await _accountRepository.GetAccountByIdAsync(request.FromAccountId);
            var toAccount = await _accountRepository.GetAccountByIdAsync(request.ToAccountId);


            if (fromAccount == null)
            {
                throw new KeyNotFoundException($"Счет-отправитель с ID {request.FromAccountId} не найден.");
            }

            if (toAccount == null)
            {
                throw new KeyNotFoundException($"Счет-получатель с ID {request.ToAccountId} не найден.");
            }


            if (fromAccount.Currency != toAccount.Currency)
            {
                throw new InvalidOperationException("Переводы возможны только между счетами в одной валюте.");
            }

            if (fromAccount.Balance < request.Amount)
            {
                throw new InvalidOperationException("Недостаточно средств на счете-отправителе.");
            }


            if (request.Amount <= 0)
            {
                throw new ArgumentException("Сумма перевода должна быть положительной.");
            }

            // Логикеа 

            fromAccount.Balance -= request.Amount;
            var debitTransaction = new Transaction
            {
                Id = Guid.NewGuid(),
                AccountId = fromAccount.Id,
                counterpartyAccountId = toAccount.Id,
                Amount = request.Amount,
                Currency = fromAccount.Currency,
                TransactionType = TransactionType.Debit,
                MetaData = request.Description,
                TransactionDate = DateTime.UtcNow
            };
            fromAccount.Transactions.Add(debitTransaction);

            // Зачисление на счет
            toAccount.Balance += request.Amount;
            var creditTransaction = new Transaction
            {
                Id = Guid.NewGuid(),
                AccountId = toAccount.Id,
                counterpartyAccountId = fromAccount.Id,
                Amount = request.Amount,
                Currency = toAccount.Currency,
                TransactionType = TransactionType.Credit,
                MetaData = request.Description,
                TransactionDate = DateTime.UtcNow
            };
            toAccount.Transactions.Add(creditTransaction);


            await _accountRepository.UpdateAsync(fromAccount);
            await _accountRepository.UpdateAsync(toAccount);
        }
    }
}