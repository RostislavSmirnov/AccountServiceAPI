namespace BankAccountServiceAPI.Infrastructure.MockCustomerVerification
{
    public interface IMockCustomerVerification
    {
        Task<bool> CustomerExistAsync(Guid Id);
    }
}
