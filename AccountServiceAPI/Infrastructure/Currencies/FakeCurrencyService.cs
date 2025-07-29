namespace AccountServiceAPI.Infrastructure.Currencies
{
    public class FakeCurrencyService : ICurrencyService
    {
        private static readonly HashSet<string> _supportedCurrencies = new(StringComparer.OrdinalIgnoreCase)
        {
            "RUB",
            "USD",
            "EUR"
        };

        public Task<bool> IsCurrencySupportedAsync(string currencyCode)
        {
            bool isSupported = _supportedCurrencies.Contains(currencyCode);
            return Task.FromResult(isSupported);
        }
    }
}