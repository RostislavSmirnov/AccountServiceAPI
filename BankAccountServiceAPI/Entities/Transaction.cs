using System.ComponentModel.DataAnnotations;
using BankAccountServiceAPI.Entities.Enums;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace BankAccountServiceAPI.Entities
{
    /// <summary>
    /// Модель данных, представляющая Транзакцию
    /// </summary>
    public class Transaction
    {
        /// <summary>
        /// Уникальный идентификатор транзакции
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Уникальный идентификатор счёта отправителя
        /// </summary>
        public Guid AccountId { get; set; }

        /// <summary>
        /// Уникальный идентификатор счёта получателя
        /// </summary>
        public Guid CounterPartyAccountId { get; set; } // Учетная запись контрагента(Куда переводят деньги)

        /// <summary>
        /// Сумма перевода
        /// </summary>
        public decimal Amount { get; set; } //Сумма перевода

        /// <summary>
        /// Код валюты из трёх букв
        /// </summary>
        [Required]
        [MaxLength(3)]
        public string CurrencyCodeISO { get; set; }

        /// <summary>
        /// Тип транзакции Credit/Debit
        /// </summary>
        public TransactionType TransactionType {get; set;}

        /// <summary>
        /// Входящая, или исходящая транзакция
        /// </summary>
        public TransactionInOrOut TransactionInOrOut { get; set; }

        /// <summary>
        /// Комментарий к транзакции
        /// </summary>
        public string? MetaData { get; set; }

        /// <summary>
        /// Дата транзакции
        /// </summary>
        public DateTime CreatedDate { get; set; }
    }
}
