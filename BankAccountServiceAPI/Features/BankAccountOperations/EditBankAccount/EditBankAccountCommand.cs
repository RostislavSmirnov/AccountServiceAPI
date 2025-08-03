using BankAccountServiceAPI.Common;
using MediatR;

namespace BankAccountServiceAPI.Features.BankAccountOperations.EditBankAccount
{
    public class EditBankAccountCommand : IRequest<MbResult<BankAccountDto>>
    {
        public Guid Id { get; set; }
        public string? CurrencyCodeISO { get; set; }
        public decimal? interestRate { get; set; }
        
    }
}
