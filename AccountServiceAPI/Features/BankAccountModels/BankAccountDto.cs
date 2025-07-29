namespace AccountServiceAPI.Features.BankAccountModels
{
    public class BankAccountDto
    {
        public Guid Id { get; set; }
        public Guid OwnerId { get; set; }
        public string AccountType { get; set; }
        public string Currency { get; set; }
        public decimal Balance { get; set; }
        public decimal? ProcentRate { get; set; }
        public DateTime OpenDate { get; set; }
        public DateTime? ClosedDate { get; set; }
    }
}