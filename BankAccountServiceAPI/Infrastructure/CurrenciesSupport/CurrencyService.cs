
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
namespace BankAccountServiceAPI.Infrastructure.CurrenciesSupport
{
    /// <summary>
    /// Сервис для проверки валюты
    /// </summary>
    public class CurrencyService : ICurrencyService
    {
        private static readonly HashSet<string> _supportedCurrencies = new(StringComparer.OrdinalIgnoreCase)
        {
            "RUB",
            "USD",
            "EUR"
        };
        
        public Task<bool> ThisCurrencyIsSupported(string currencyCode)
        {
            var isSupported = _supportedCurrencies.Contains(currencyCode);
            return Task.FromResult(isSupported);
        }
    }
}
