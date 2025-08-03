using BankAccountServiceAPI.Common;
using BankAccountServiceAPI.Entities;
using BankAccountServiceAPI.Infrastructure.MockRepository;
using MediatR;

namespace BankAccountServiceAPI.Features.BankAccountOperations.DeleteBankAccount
{
    public class DeletebankAccountCommandHandler : IRequestHandler<DeleteBankAccountCommand, MbResult>
    {
        public IMockBankAccountRepository _bankAccountRepository;

        public DeletebankAccountCommandHandler(IMockBankAccountRepository bankAccountRepository)
        {
            _bankAccountRepository = bankAccountRepository;
        }

        public async Task<MbResult> Handle(DeleteBankAccountCommand request, CancellationToken cancellationToken)
        {
            BankAccount? account = await _bankAccountRepository.GetBankAccountById(request.Id);
            
            if (account == null)
            {
                return MbResult.Failure(new MbError("NotFound", $"Счет с ID {request.Id} не найден."));
            }

            if (account.Balance != 0)
            {
                return MbResult.Failure(new MbError("NonZeroBalance", $"Нельзя удалить счет с ID {request.Id}, так как на нем ненулевой баланс."));
            }

            try
            {
                await _bankAccountRepository.DeleteBankAccount(request.Id);
                return MbResult.Success();
            }
            catch (Exception ex)
            {
                return MbResult.Failure(new MbError("DeletionFailed", "Произошла внутренняя ошибка при удалении счета."));
            }
        }
    }
}
