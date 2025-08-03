using System.ComponentModel.DataAnnotations;
using BankAccountServiceAPI.Entities.Enums;

namespace BankAccountServiceAPI.Entities
{
    public class Transaction
    {
        public Guid Id { get; set; }
        public Guid AccountId { get; set; }

        public Guid CounterPartyAccountId { get; set; } // Учетная запись контрагента(Куда переводят деньги)

        public decimal Amount { get; set; } //Сумма перевода

        [Required]
        [MaxLength(3)]
        public string CurrencyCodeISO { get; set; }

        public TransactionType TransactionType {get; set;}

        public TransactionInOrOut TransactionInOrOut { get; set; }

        public string MetaData { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
