
namespace BankAccountServiceAPI.Infrastructure.MockCustomerVerification
{
    public class MockCustomerVerification : IMockCustomerVerification
    {
        private static readonly HashSet<Guid> _existingCustomerId = new()
        {
            Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
            Guid.Parse("11212344-5566-7788-93AA-445566778899"),
            Guid.Parse("18823344-6566-7788-99AA-BBCCDDEEFF00"),
            Guid.Parse("11233344-5566-7788-99AA-BBCCDDEEFF78")
        };
        public Task<bool> CustomerExistAsync(Guid Id)
        {
            bool exists = _existingCustomerId.Contains(Id);
            return Task.FromResult(exists);
        }
    }
}
