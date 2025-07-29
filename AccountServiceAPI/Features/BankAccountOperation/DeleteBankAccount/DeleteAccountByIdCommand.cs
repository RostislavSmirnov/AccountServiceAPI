using MediatR;

namespace AccountServiceAPI.Features.BankAccountOperation.DeleteBankAccount
{
    public class DeleteAccountByIdCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}