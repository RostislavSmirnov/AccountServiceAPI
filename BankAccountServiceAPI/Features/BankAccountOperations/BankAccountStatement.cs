using BankAccountServiceAPI.Features.TransferOperations;

namespace BankAccountServiceAPI.Features.BankAccountOperations
{
    public class BankAccountStatement
    {
        public Guid AccountId { get; set; }
        public Guid OwnerId { get; set; }
        public decimal CurrentBalance { get; set; }
        public string Currency { get; set; }
        public DateTime StatementFrom { get; set; }
        public DateTime StatementTo { get; set; }
        public List<ShowTransactionDto> Transactions { get; set; } = new();
    }
}
