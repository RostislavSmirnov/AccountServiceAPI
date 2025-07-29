using AccountServiceAPI.Infrastructure.Persistence;
using MediatR;

namespace AccountServiceAPI.Features.BankAccountOperation.DeleteBankAccount
{
    public class DeleteAccountCommandHandler : IRequestHandler<DeleteAccountByIdCommand>
    {
        private readonly IAccountRepository _accountRepository;

        public DeleteAccountCommandHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task Handle(DeleteAccountByIdCommand request, CancellationToken cancellationToken)
        {
            var account = await _accountRepository.GetAccountByIdAsync(request.Id);
            if (account == null)
            {
                throw new Exception($"Счет с ID {request.Id} не найден.");
            }

            if (account.Balance != 0)
            {
                throw new InvalidOperationException("Нельзя удалить счет с ненулевым балансом.");
            }

            await _accountRepository.DeleteAsync(request.Id);
        }
    }
}