namespace AccountServiceAPI.Features.BankAccountModels
{
    public class TransactionDto
    {
        public Guid Id { get; set; }
        public Guid? CounterpartyAccountId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public DateTime Timestamp { get; set; }
    }
}