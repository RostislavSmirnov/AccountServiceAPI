using BankAccountServiceAPI.Entities.Enums;
using MediatR;
using BankAccountServiceAPI.Common;

namespace BankAccountServiceAPI.Features.BankAccountOperations.CreateBankAccount
{
    public class CreateBankAccountCommand : IRequest<MbResult<Guid>>
    {
        public Guid OwnerId { get; set; } //Id владельца счёта

        public AccountType AccountType { get; set; }

        public string CurrencyCodeISO { get; set; }

        public decimal? InterestRate { get; set; } //Процентная ставка
    }
}
