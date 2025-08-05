using BankAccountServiceAPI.Common;
using MediatR;

namespace BankAccountServiceAPI.Features.BankAccountOperations.GetBankAccounts
{
    /// <summary>
    /// Запрос вывода всех счетов
    /// </summary>
    public class GetBankAccountsQuery : IRequest<MbResult<IEnumerable<BankAccountDto>>>
    {
    }
}
