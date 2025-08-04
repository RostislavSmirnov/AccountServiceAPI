using BankAccountServiceAPI.Common;
using MediatR;

namespace BankAccountServiceAPI.Features.BankAccountOperations.DeleteBankAccount
{
    public class DeleteBankAccountCommand:IRequest<MbResult>
    {
        /// <summary>
        /// Уникальный индентификатор счёта который необходимо удалить
        /// </summary>
        public Guid Id { get; set; }
    }
}
