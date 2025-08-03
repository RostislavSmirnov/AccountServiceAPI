using BankAccountServiceAPI.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace BankAccountServiceAPI.Features.BankAccountOperations
{
    public class BankAccountDto
    {
        public Guid Id { get; set; } //Id Счёта

        public Guid OwnerId { get; set; } //Id владельца счёта

        public AccountType AccountType { get; set; }

        public string CurrencyCodeISO { get; set; }

        public decimal Balance { get; set; }

        public decimal InterestRate { get; set; } //Процентная ставка

        public DateTime OpenDate { get; set; }

        public DateTime? CloseDate { get; set; }
    }
}
