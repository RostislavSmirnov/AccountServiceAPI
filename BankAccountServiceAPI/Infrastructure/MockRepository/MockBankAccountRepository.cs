using System.Collections.Concurrent;
using BankAccountServiceAPI.Entities;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace BankAccountServiceAPI.Infrastructure.MockRepository
{
    /// <summary>
    /// Клсс описывающий CRUD операции в репозитории
    /// </summary>
    public class MockBankAccountRepository : IMockBankAccountRepository
    {
        private readonly ConcurrentDictionary<Guid, BankAccount> _mockRepository = new ConcurrentDictionary<Guid, BankAccount>();


        public Task<BankAccount> CreateBankAccount(BankAccount account)
        {
            account.Id = Guid.NewGuid();
            _mockRepository[account.Id] = account;
            account.Balance = 5000;
            return Task.FromResult(account);
        }


        public Task<Guid> DeleteBankAccount(Guid idBancAccount)
        {
            _mockRepository.TryRemove(idBancAccount, out _);
            return Task.FromResult(idBancAccount);
        }


        public Task<BankAccount> EditBankAccount(BankAccount bankAccountForUpdate)
        {
            if (!_mockRepository.TryGetValue(bankAccountForUpdate.Id, out var existingAccount))
            {
                throw new Exception($"Счёт с ID {bankAccountForUpdate.Id} не найден");
            }

            if (existingAccount.CurrencyCodeISO != bankAccountForUpdate.CurrencyCodeISO && existingAccount.Balance != 0)
            {
                throw new Exception("Нельзя изменить валюту счета с ненулевым балансом.");
            }

            existingAccount.CurrencyCodeISO = bankAccountForUpdate.CurrencyCodeISO;
            existingAccount.InterestRate = bankAccountForUpdate.InterestRate;
            //existingAccount.CloseDate = bankAccountForUpdate.CloseDate;

            _mockRepository[existingAccount.Id] = existingAccount;

            return Task.FromResult(existingAccount);
        }


        public async Task<IEnumerable<BankAccount>> GetAllBankAccounts()
        {
            return await Task.FromResult<IEnumerable<BankAccount>>(_mockRepository.Values);
        }


        public async Task<BankAccount> GetBankAccountById(Guid id)
        {
            if (!_mockRepository.TryGetValue(id, out var existingAccount))
            {
                throw new Exception($"Счёт с ID {id} не найден");
            }
            return await Task.FromResult(existingAccount);
        }
    }
}
