using AccountServiceAPI.Entities;

namespace AccountServiceAPI.Infrastructure.Persistence
{
    public interface IAccountRepository
    {
        Task<IEnumerable<BankAccount>> GetAllAccountsAsync();

        Task<BankAccount> GetAccountByIdAsync(Guid accountId);

        Task<BankAccount> CreateAccountAsync(BankAccount account);

        Task UpdateAsync(BankAccount account);

        Task DeleteAsync(Guid accountId);
    }
}