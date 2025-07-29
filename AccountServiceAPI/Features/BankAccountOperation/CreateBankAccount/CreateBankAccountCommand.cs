using AccountServiceAPI.Entities.Enums;
using MediatR;

namespace AccountServiceAPI.Features.BankAccountOperation.CreateBankAccount
{
    public class CreateBankAccountCommand : IRequest<Guid>
    {
        public Guid OwnerId { get; set; }
        public AccountType AccountType { get; set; }
        public string Currency { get; set; }
        public decimal? ProcentRate { get; set; }
    }
}