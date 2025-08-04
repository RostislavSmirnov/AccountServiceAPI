using BankAccountServiceAPI.Common;
using MediatR;

namespace BankAccountServiceAPI.Features.BankAccountOperations.GetBankAccountById
{
    public class GetBankAccountQuery : IRequest<MbResult<BankAccountDto>>
    {
        /// <summary>
        /// Уникальный идентификатор счёта, котоырый необходимо найти.
        /// </summary>
        public Guid Id { get; set; }
    }
}
