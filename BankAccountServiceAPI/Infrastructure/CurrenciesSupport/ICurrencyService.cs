#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
namespace BankAccountServiceAPI.Infrastructure.CurrenciesSupport
{
    /// <summary>
    /// Инте
    /// </summary>
    public interface ICurrencyService
    {

        Task<bool> ThisCurrencyIsSupported(string currencyCode);
    }
}
