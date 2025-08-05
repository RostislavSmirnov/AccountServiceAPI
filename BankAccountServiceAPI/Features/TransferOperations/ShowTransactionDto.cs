using BankAccountServiceAPI.Entities.Enums;

namespace BankAccountServiceAPI.Features.TransferOperations
{
    /// <summary>
    /// Класс для демонстрацими транзакции
    /// </summary>
    public class ShowTransactionDto
    {
        /// <summary>
        /// Уникальный идентификатор транзакции
        /// </summary>
        public Guid Id { get; init; }

        /// <summary>
        /// Уникальный идентификатор счёта с которого совершается перевод
        /// </summary>
        public Guid AccountId { get; init; }


        /// <summary>
        /// Уникальный идентификатор счёта на который совершается перевод
        /// </summary>
        public Guid CounterPartyAccountId { get; init; } // Учетная запись контрагента(Куда переводят деньги)

        /// <summary>
        /// Сумма перевода
        /// </summary>
        public decimal Amount { get; init; } //Сумма перевода

        /// <summary>
        /// Указание валюты перевода в формате из трёх букв (Например RUB, USD)
        /// </summary>
        public required string CurrencyCodeISO { get; init; }

        /// <summary>
        /// Указание какой тип перевода, зависит от типа счёта (Credit, Debit)
        /// </summary>
        public TransactionType TransactionType { get; init; }

        /// <summary>
        /// Указание типа перевода, входящий, или исходящий
        /// </summary>
        public TransactionInOrOut TransactionInOrOut { get; init; }

        /// <summary>
        /// Комментарии
        /// </summary>
        public string MetaData { get; init; } = null!;

        /// <summary>
        /// Дата перевода
        /// </summary>
        public DateTime CreatedDate { get; init; }
    }
}
