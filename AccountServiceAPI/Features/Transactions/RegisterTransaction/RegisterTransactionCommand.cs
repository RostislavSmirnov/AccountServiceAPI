using AccountServiceAPI.Entities.Enums;
using MediatR;

namespace AccountServiceAPI.Features.Transactions.RegisterTransaction
{
    public class RegisterTransactionCommand : IRequest<Guid>
    {
        public Guid AccountId { get; set; }
        public decimal Amount { get; set; }
        public TransactionType Type { get; set; }
        public string Description { get; set; }
    }
}