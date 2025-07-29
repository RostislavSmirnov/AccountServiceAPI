namespace AccountServiceAPI.Infrastructure.CustomerVerification
{
    public class FakeCustomerVerificationService : ICustomerVerificationService
    {
        private static readonly HashSet<Guid> _existingCustomerIds = new()
        {
            Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
            Guid.Parse("AABBCCDD-EEFF-0011-2233-445566778899")
        };

        public Task<bool> CustomerExistsAsync(Guid ownerId)
        {
            bool exists = _existingCustomerIds.Contains(ownerId);
            return Task.FromResult(exists);
        }
    }
}