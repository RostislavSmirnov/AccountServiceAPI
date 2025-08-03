using BankAccountServiceAPI.Common;
using MediatR;

namespace BankAccountServiceAPI.Features.BankAccountOperations.GetBankAccountStatement
{
    public class GetBankAccountStatementQuery : IRequest<MbResult<BankAccountStatement>>
    {
        /// <summary>
        /// Уникальный идентификатор счёта по которому надо сделать выписку.
        /// </summary>
        public Guid AccountId { get; set; }

        /// <summary>
        /// Дата с которой необходимо учитывать переводы.
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// По какую дату надо учитывать переводы.
        /// </summary>
        public DateTime EndDate { get; set; }
    }
}
