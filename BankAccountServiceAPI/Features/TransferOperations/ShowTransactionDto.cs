using BankAccountServiceAPI.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace BankAccountServiceAPI.Features.TransferOperations
{
    public class ShowTransactionDto
    {
        public Guid Id { get; set; }
        public Guid AccountId { get; set; }

        public Guid CounterPartyAccountId { get; set; } // Учетная запись контрагента(Куда переводят деньги)

        public decimal Amount { get; set; } //Сумма перевода

        public string CurrencyCodeISO { get; set; }

        public TransactionType TransactionType { get; set; }

        public TransactionInOrOut TransactionInOrOut { get; set; }

        public string MetaData { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
