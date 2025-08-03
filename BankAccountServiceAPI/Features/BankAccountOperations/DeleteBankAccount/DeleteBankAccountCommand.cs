using BankAccountServiceAPI.Common;
using MediatR;

namespace BankAccountServiceAPI.Features.BankAccountOperations.DeleteBankAccount
{
    public class DeleteBankAccountCommand:IRequest<MbResult>
    {
        public Guid Id { get; set; }
    }
}
