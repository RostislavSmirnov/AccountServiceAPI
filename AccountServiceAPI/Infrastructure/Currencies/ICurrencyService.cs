namespace AccountServiceAPI.Infrastructure.Currencies
{
    public interface ICurrencyService
    {
        Task<bool> IsCurrencySupportedAsync(string currencyCode);
    }
}