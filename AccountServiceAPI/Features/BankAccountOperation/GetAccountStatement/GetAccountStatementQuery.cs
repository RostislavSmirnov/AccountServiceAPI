using AccountServiceAPI.Features.BankAccountModels;
using MediatR;

namespace AccountServiceAPI.Features.BankAccountOperation.GetAccountStatement
{
    public class GetAccountStatementQuery : IRequest<AccountStatementDto>
    {
        public Guid AccountId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}