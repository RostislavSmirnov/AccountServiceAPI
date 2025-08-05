#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
namespace BankAccountServiceAPI.Infrastructure.MockCustomerVerification
{
    /// <summary>
    /// Интерфейс заглушки верификации
    /// </summary>
    public interface IMockCustomerVerification
    {
        Task<bool> CustomerExistAsync(Guid Id);
    }
}
