using BankAccountServiceAPI.Entities;
using BankAccountServiceAPI.Features.BankAccountOperations;

namespace BankAccountServiceAPI.Infrastructure.MockRepository
{
    public interface IMockBankAccountRepository
    {
        Task<BankAccount> CreateBankAccount(BankAccount account);

        Task<Guid> DeleteBankAccount(Guid idBancAccount);

        Task<BankAccount> EditBankAccount(BankAccount bankAccountForUpdate);

        Task<IEnumerable<BankAccount>> GetAllBankAccounts();

        Task<BankAccount> GetBankAccountById(Guid id);
    }
}
