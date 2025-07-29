using AccountServiceAPI.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace AccountServiceAPI.Entities
{
    public class Transaction
    {
        public Guid Id { get; set; }
        public Guid AccountId { get; set; }
        public Guid counterpartyAccountId { get; set; }

        public decimal Amount { get; set; }

        [Required]
        [StringLength(3)]
        public string Currency { get; set; }

        public TransactionType TransactionType { get; set; }

        public string MetaData { get; set; }

        public DateTime TransactionDate { get; set; }
    }
}