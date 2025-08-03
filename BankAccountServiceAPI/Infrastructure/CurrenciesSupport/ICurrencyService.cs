namespace BankAccountServiceAPI.Infrastructure.CurrenciesSupport
{
    public interface ICurrencyService
    {
        Task<bool> ThisCurrencyIsSupported(string currencyCode);
    }
}
