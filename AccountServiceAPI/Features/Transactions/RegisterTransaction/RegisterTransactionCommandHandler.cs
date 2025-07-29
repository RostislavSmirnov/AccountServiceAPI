using AccountServiceAPI.Entities;
using AccountServiceAPI.Entities.Enums;
using AccountServiceAPI.Infrastructure.Persistence;
using MediatR;

namespace AccountServiceAPI.Features.Transactions.RegisterTransaction
{
    public class RegisterTransactionCommandHandler : IRequestHandler<RegisterTransactionCommand, Guid>
    {
        private readonly IAccountRepository _accountRepository;
        // В будущем может понадобиться ITransactionRepository

        public RegisterTransactionCommandHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<Guid> Handle(RegisterTransactionCommand request, CancellationToken cancellationToken)
        {
            var account = await _accountRepository.GetAccountByIdAsync(request.AccountId);
            if (account == null)
            {
                throw new Exception($"Счет с ID {request.AccountId} не найден.");
            }

            // --- Бизнес-логика ---
            if (request.Type == TransactionType.Debit && account.Balance < request.Amount)
            {
                throw new InvalidOperationException("Недостаточно средств на счете.");
            }

            // Обновляем баланс счета
            account.Balance += (request.Type == TransactionType.Credit ? request.Amount : -request.Amount);

            // Создаем транзакцию
            var transaction = new Transaction
            {
                Id = Guid.NewGuid(),
                AccountId = request.AccountId,
                Amount = request.Amount,
                Currency = account.Currency,
                TransactionType = request.Type,
                MetaData = request.Description,
                TransactionDate = DateTime.UtcNow
            };

            account.Transactions.Add(transaction);

            await _accountRepository.UpdateAsync(account);

            return transaction.Id;
        }
    }
}