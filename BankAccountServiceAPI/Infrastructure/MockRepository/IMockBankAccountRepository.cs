using BankAccountServiceAPI.Entities;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace BankAccountServiceAPI.Infrastructure.MockRepository
{
    /// <summary>
    /// Интерфейс заглушки  репозитория создания счетов
    /// </summary>
    public interface IMockBankAccountRepository
    {
        Task<BankAccount> CreateBankAccount(BankAccount account);

        Task<Guid> DeleteBankAccount(Guid idBancAccount);

        Task<BankAccount> EditBankAccount(BankAccount bankAccountForUpdate);

        Task<IEnumerable<BankAccount>> GetAllBankAccounts();

        Task<BankAccount> GetBankAccountById(Guid id);
    }
}
