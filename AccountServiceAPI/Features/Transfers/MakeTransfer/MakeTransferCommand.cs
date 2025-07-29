using MediatR;

namespace AccountServiceAPI.Features.Transfers.MakeTransfer
{
    public class MakeTransferCommand : IRequest
    {
        public Guid FromAccountId { get; set; }
        public Guid ToAccountId { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
    }
}