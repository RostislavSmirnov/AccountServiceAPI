using BankAccountServiceAPI.Common;
using MediatR;

namespace BankAccountServiceAPI.Features.BankAccountOperations.GetBankAccountById
{   
    /// <summary>
    /// Запрос выдачи счёта по ID
    /// </summary>
    public class GetBankAccountQuery : IRequest<MbResult<BankAccountDto>>
    {
        /// <summary>
        /// Уникальный идентификатор счёта, котоырый необходимо найти.
        /// </summary>
        public Guid Id { get; init; }
    }
}
