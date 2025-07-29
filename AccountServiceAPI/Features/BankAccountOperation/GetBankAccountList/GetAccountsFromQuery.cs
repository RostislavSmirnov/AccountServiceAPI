using AccountServiceAPI.Features.BankAccountModels;
using MediatR;

namespace AccountServiceAPI.Features.BankAccountOperation.GetBankAccountList
{
    public class GetAccountsFromQuery : IRequest<IEnumerable<BankAccountDto>>
    {
    }
}