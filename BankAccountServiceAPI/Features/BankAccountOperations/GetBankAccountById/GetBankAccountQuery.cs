using BankAccountServiceAPI.Common;
using MediatR;

namespace BankAccountServiceAPI.Features.BankAccountOperations.GetBankAccountById
{
    public class GetBankAccountQuery : IRequest<MbResult<BankAccountDto>>
    {
        public Guid Id { get; set; }
    }
}
