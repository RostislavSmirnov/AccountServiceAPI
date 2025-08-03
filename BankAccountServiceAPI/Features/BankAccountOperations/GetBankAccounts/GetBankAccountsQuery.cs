using BankAccountServiceAPI.Common;
using MediatR;

namespace BankAccountServiceAPI.Features.BankAccountOperations.GetBankAccounts
{
    public class GetBankAccountsQuery : IRequest<MbResult<IEnumerable<BankAccountDto>>>
    {
    }
}
