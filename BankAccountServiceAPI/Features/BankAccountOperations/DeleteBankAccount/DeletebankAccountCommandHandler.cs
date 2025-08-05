using BankAccountServiceAPI.Common;
using BankAccountServiceAPI.Entities;
using BankAccountServiceAPI.Infrastructure.MockRepository;
using MediatR;
// ReSharper disable SuggestVarOrType_SimpleTypes Я предпочитаю явную типизацию в большинстве случаев, так как мне так легче понимать код.
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace BankAccountServiceAPI.Features.BankAccountOperations.DeleteBankAccount
{
    /// <summary>
    /// Класс обработки комманды удаления счёта
    /// </summary>
    public class DeleteBankAccountCommandHandler : IRequestHandler<DeleteBankAccountCommand, MbResult>
    {
        private readonly IMockBankAccountRepository _bankAccountRepository;

        // ReSharper disable once ConvertToPrimaryConstructor считаю обычный конструктор более читаемым
        public DeleteBankAccountCommandHandler(IMockBankAccountRepository bankAccountRepository)
        {
            _bankAccountRepository = bankAccountRepository;
        }

        public async Task<MbResult> Handle(DeleteBankAccountCommand request, CancellationToken cancellationToken)
        {
            BankAccount account = await _bankAccountRepository.GetBankAccountById(request.Id);

            if (account.Balance != 0)
            {
                return MbResult.Failure(new MbError("NonZeroBalance", $"Нельзя удалить счет с ID {request.Id}, так как на нем ненулевой баланс."));
            }

            try
            {
                await _bankAccountRepository.DeleteBankAccount(request.Id);
                return MbResult.Success();
            }
            catch (Exception)
            {
                return MbResult.Failure(new MbError("DeletionFailed", "Произошла внутренняя ошибка при удалении счета."));
            }
        }
    }
}
