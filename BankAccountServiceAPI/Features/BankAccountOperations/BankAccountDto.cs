using BankAccountServiceAPI.Entities.Enums;

namespace BankAccountServiceAPI.Features.BankAccountOperations
{
    /// <summary>
    /// DTO для показа информации о счёте
    /// </summary>
    public class BankAccountDto
    {
        /// <summary>
        /// Идентификатор счёта
        /// </summary>
        public Guid Id { get; init; } //Id Счёта

        /// <summary>
        /// Идентификатор владельца счёта
        /// </summary>
        public Guid OwnerId { get; init; } 

        /// <summary>
        /// Тип счёта, например кредитный, депозит, стандартный
        /// </summary>
        public AccountType AccountType { get; init; }

        /// <summary>
        /// Код валюты в формате из трёх букв
        /// </summary>
        public required string CurrencyCodeISO { get; init; }

        /// <summary>
        /// Баланс счёта
        /// </summary>
        public decimal Balance { get; init; }


        /// <summary>
        /// Процент на депозитном счёте
        /// </summary>
        public decimal InterestRate { get; init; } //Процентная ставка

        /// <summary>
        /// Дата открытия счёта
        /// </summary>
        public DateTime OpenDate { get; init; }

        /// <summary>
        /// Дата закрытия
        /// </summary>
        public DateTime? CloseDate { get; init; }
    }
}
