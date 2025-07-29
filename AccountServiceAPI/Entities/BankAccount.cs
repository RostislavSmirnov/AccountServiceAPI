using AccountServiceAPI.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace AccountServiceAPI.Entities
{
    public class BankAccount
    {
        public Guid Id { get; set; }
        public Guid OwnerId { get; set; }

        [Required]
        public AccountType AccountType { get; set; }

        [Required]
        [StringLength(3)]
        public string Currency { get; set; }

        [Required]
        public decimal Balance { get; set; }

        public decimal? ProcentRate { get; set; }

        public DateTime OpenDate { get; set; }

        public DateTime ClosedDate { get; set; }

        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}