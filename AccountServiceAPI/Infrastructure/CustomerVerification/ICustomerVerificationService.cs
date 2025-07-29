namespace AccountServiceAPI.Infrastructure.CustomerVerification
{
    public interface ICustomerVerificationService
    {
        Task<bool> CustomerExistsAsync(Guid customerId);
    }
}