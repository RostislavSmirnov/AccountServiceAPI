using BankAccountServiceAPI.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace BankAccountServiceAPI.Entities
{
    /// <summary>
    /// Модель данных, представляющая банковский счет (Data Transfer Object).
    /// </summary>
    public class BankAccount
    {
        /// <summary>
        /// Уникальный идентификатор счета (GUID).
        /// </summary>
        public Guid Id { get; set; } //Id Счёта

        /// <summary>
        /// Идентификатор владельца счета.
        /// </summary>
        public Guid OwnerId { get; init; } //Id владельца счёта

        /// <summary>
        /// Тип счета (Checking, Deposit, Credit).
        /// </summary>
        public AccountType AccountType { get; init; }

        /// <summary>
        /// Трехбуквенный код валюты (ISO 4217).
        /// </summary>
        [Required]
        [MaxLength(3)]
        public required string CurrencyCodeISO { get; set; }

        /// <summary>
        /// Текущий баланс счета.
        /// </summary>
        public decimal Balance { get; set; }

        /// <summary>
        /// Процентная ставка. Применяется для вкладов (Deposit) и кредитных счетов (Credit).
        /// </summary>
        public decimal? InterestRate { get; set; } //Процентная ставка

        /// <summary>
        /// Дата и время открытия счета.
        /// </summary>
        public DateTime OpenDate { get; init; }

        /// <summary>
        /// Дата и время закрытия счета (может отсутствовать, если счет активен).
        /// </summary>
        public DateTime CloseDate { get; init; }

        /// <summary>
        /// Коллекция транзакций
        /// </summary>
        public ICollection<Transaction> Transactions = new List<Transaction>();
    }
}
