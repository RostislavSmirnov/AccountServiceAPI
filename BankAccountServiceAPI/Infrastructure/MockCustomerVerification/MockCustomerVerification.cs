
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
namespace BankAccountServiceAPI.Infrastructure.MockCustomerVerification
{
    /// <summary>
    /// Класс заглушки верификации
    /// </summary>
    public class MockCustomerVerification : IMockCustomerVerification
    {
        private static readonly HashSet<Guid> _existingCustomerId =
        [
            Guid.Parse("11223344-5566-7788-99AA-BBCCDDEEFF00"),
            Guid.Parse("11212344-5566-7788-93AA-445566778899"),
            Guid.Parse("18823344-6566-7788-99AA-BBCCDDEEFF00"),
            Guid.Parse("11233344-5566-7788-99AA-BBCCDDEEFF78")
        ];
        public Task<bool> CustomerExistAsync(Guid Id)
        {
            var exists = _existingCustomerId.Contains(Id);
            return Task.FromResult(exists);
        }
    }
}
