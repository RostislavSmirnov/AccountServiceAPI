using BankAccountServiceAPI.Common;
using MediatR;

namespace BankAccountServiceAPI.Features.BankAccountOperations.DeleteBankAccount
{
    /// <summary>
    /// Команда удаления 
    /// </summary>
    public class DeleteBankAccountCommand:IRequest<MbResult>
    {
        /// <summary>
        /// Уникальный индентификатор счёта который необходимо удалить
        /// </summary>
        public Guid Id { get; init; }
    }
}
