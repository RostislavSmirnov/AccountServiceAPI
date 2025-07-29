using AccountServiceAPI.Features.BankAccountModels;
using MediatR;

namespace AccountServiceAPI.Features.BankAccountOperation.GetBankAccountById
{
    public class GetAccountByIdQuery : IRequest<BankAccountDto>
    {
        public Guid Id { get; set; }
    }
}