using BankAccountServiceAPI.Features.TransferOperations;

namespace BankAccountServiceAPI.Features.BankAccountOperations
{   
    /// <summary>
    /// Класс описывающий содержание банковской выписки
    /// </summary>
    public class BankAccountStatement
    {
        /// <summary>
        /// Уникальный Id Счёта.
        /// </summary>
        public Guid AccountId { get; init; }

        /// <summary>
        /// Уникальный Id владельца счёта.
        /// </summary>
        public Guid OwnerId { get; init; }

        /// <summary>
        /// Баланс счёта
        /// </summary>
        public decimal CurrentBalance { get; init; }

        /// <summary>
        /// Валюта счёта.
        /// </summary>
        public required string Currency { get; init; }

        /// <summary>
        /// Дата с которой происходит отбор транзакций.
        /// </summary>
        public DateTime StatementFrom { get; init; }

        /// <summary>
        /// Дата по которую происходит отбор транзакций.
        /// </summary>
        public DateTime StatementTo { get; init; }

        /// <summary>
        /// Коллекция транзакций.
        /// </summary>
        public List<ShowTransactionDto> Transactions { get; init; } = [];
    }
}
