using BankAccountServiceAPI.Features.TransferOperations;

namespace BankAccountServiceAPI.Features.BankAccountOperations
{
    public class BankAccountStatement
    {
        /// <summary>
        /// Уникальный Id Счёта.
        /// </summary>
        public Guid AccountId { get; set; }

        /// <summary>
        /// Уникальный Id владельца счёта.
        /// </summary>
        public Guid OwnerId { get; set; }

        /// <summary>
        /// Баланс счёта
        /// </summary>
        public decimal CurrentBalance { get; set; }

        /// <summary>
        /// Валюта счёта.
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// Дата с которой происходит отбор транзакций.
        /// </summary>
        public DateTime StatementFrom { get; set; }

        /// <summary>
        /// Дата по которую происходит отбор транзакций.
        /// </summary>
        public DateTime StatementTo { get; set; }

        /// <summary>
        /// Коллекция транзакций.
        /// </summary>
        public List<ShowTransactionDto> Transactions { get; set; } = new();
    }
}
