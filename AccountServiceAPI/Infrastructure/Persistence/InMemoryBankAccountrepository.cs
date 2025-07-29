using AccountServiceAPI.Entities;
using System.Collections.Concurrent;

namespace AccountServiceAPI.Infrastructure.Persistence
{
    public class InMemoryBankAccountrepository : IAccountRepository
    {
        private static readonly ConcurrentDictionary<Guid, BankAccount> _accounts = new ConcurrentDictionary<Guid, BankAccount>();

        public Task<BankAccount> CreateAccountAsync(BankAccount account)
        {
            account.Id = Guid.NewGuid();
            _accounts[account.Id] = account;
            account.Balance = 5000;
            return Task.FromResult(account);
        }

        public Task DeleteAsync(Guid accountId)
        {
            _accounts.TryRemove(accountId, out _);
            return Task.CompletedTask;
        }

        public Task<BankAccount> GetAccountByIdAsync(Guid accountId)
        {
            _accounts.TryGetValue(accountId, out var account);
            return Task.FromResult(account);
        }

        public Task<IEnumerable<BankAccount>> GetAllAccountsAsync()
        {
            return Task.FromResult<IEnumerable<BankAccount>>(_accounts.Values);
        }

        public Task UpdateAsync(BankAccount account)
        {
            _accounts[account.Id] = account;
            return Task.CompletedTask;
        }
    }
}