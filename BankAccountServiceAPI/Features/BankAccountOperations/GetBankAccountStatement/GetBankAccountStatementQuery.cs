using BankAccountServiceAPI.Common;
using MediatR;

namespace BankAccountServiceAPI.Features.BankAccountOperations.GetBankAccountStatement
{
    /// <summary>
    /// Класс описывающий запрос на получение выписки
    /// </summary>
    public class GetBankAccountStatementQuery : IRequest<MbResult<BankAccountStatement>>
    {
        /// <summary>
        /// Уникальный идентификатор счёта по которому надо сделать выписку.
        /// </summary>
        public Guid AccountId { get; init; }

        /// <summary>
        /// Дата с которой необходимо учитывать переводы.
        /// </summary>
        public DateTime StartDate { get; init; }

        /// <summary>
        /// По какую дату надо учитывать переводы.
        /// </summary>
        public DateTime EndDate { get; init; }
    }
}
