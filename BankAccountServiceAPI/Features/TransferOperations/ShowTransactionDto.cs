using BankAccountServiceAPI.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace BankAccountServiceAPI.Features.TransferOperations
{
    public class ShowTransactionDto
    {
        /// <summary>
        /// Уникальный идентификатор транзакции
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Уникальный идентификатор счёта с которого совершается перевод
        /// </summary>
        public Guid AccountId { get; set; }


        /// <summary>
        /// Уникальный идентификатор счёта на который совершается перевод
        /// </summary>
        public Guid CounterPartyAccountId { get; set; } // Учетная запись контрагента(Куда переводят деньги)

        /// <summary>
        /// Сумма перевода
        /// </summary>
        public decimal Amount { get; set; } //Сумма перевода

        /// <summary>
        /// Указание валюты перевода в формате из трёх букв (Например RUB, USD)
        /// </summary>
        public string CurrencyCodeISO { get; set; }

        /// <summary>
        /// Указание какой тип перевода, зависит от типа счёта (Credit, Debit)
        /// </summary>
        public TransactionType TransactionType { get; set; }

        /// <summary>
        /// Указание типа перевода, входящий, или исходящий
        /// </summary>
        public TransactionInOrOut TransactionInOrOut { get; set; }

        /// <summary>
        /// Комментарии
        /// </summary>
        public string MetaData { get; set; }

        /// <summary>
        /// Дата перевода
        /// </summary>
        public DateTime CreatedDate { get; set; }
    }
}
