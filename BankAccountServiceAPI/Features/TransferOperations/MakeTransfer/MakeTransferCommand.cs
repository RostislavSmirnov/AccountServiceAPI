using MediatR;
using BankAccountServiceAPI.Common;

namespace BankAccountServiceAPI.Features.TransferOperations.MakeTransfer
{
    /// <summary>
    /// Команда для выполнения переводов между счетами
    /// </summary>
    public class MakeTransferCommand : IRequest<MbResult<ShowTransactionDto>>
    {
        /// <summary>
        /// Уникальный идентификатор счёта с которого совершется перевод
        /// </summary>
        public Guid AccountId { get; set; }

        /// <summary>
        /// Уникальный идентификатор счёта на который совершается перевод
        /// </summary>
        public Guid CounterPartyAccountId { get; set; } 

        /// <summary>
        /// Сумма перевода
        /// </summary>
        public decimal Amount { get; set; } 

        /// <summary>
        /// Комментарии к переводу
        /// </summary>
        public string? MetaData { get; set; }
    }
}
