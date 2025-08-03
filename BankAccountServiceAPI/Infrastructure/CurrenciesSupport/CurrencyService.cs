
namespace BankAccountServiceAPI.Infrastructure.CurrenciesSupport
{
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
            bool isSupported = _supportedCurrencies.Contains(currencyCode);
            return Task.FromResult(isSupported);
        }
    }
}
