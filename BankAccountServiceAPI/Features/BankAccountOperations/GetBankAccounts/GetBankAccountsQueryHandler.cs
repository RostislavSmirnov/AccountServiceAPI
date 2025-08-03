using AutoMapper;
using BankAccountServiceAPI.Common;
using BankAccountServiceAPI.Entities;
using BankAccountServiceAPI.Infrastructure.MockRepository;
using MediatR;

namespace BankAccountServiceAPI.Features.BankAccountOperations.GetBankAccounts
{
    public class GetBankAccountsQueryHandler : IRequestHandler<GetBankAccountsQuery, MbResult<IEnumerable<BankAccountDto>>>
    {
        private readonly IMockBankAccountRepository _mockBankAcountRepository;
        private readonly IMapper _mapper;
        public GetBankAccountsQueryHandler(IMockBankAccountRepository mockBankAcountRepository, IMapper mapper)
        {
            _mockBankAcountRepository = mockBankAcountRepository;
            _mapper = mapper;
        }

        public async Task<MbResult<IEnumerable<BankAccountDto>>> Handle(GetBankAccountsQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<BankAccountDto> accounts = _mapper.Map<IEnumerable<BankAccountDto>>(await _mockBankAcountRepository.GetAllBankAccounts());
            return MbResult<IEnumerable<BankAccountDto>>.Success(accounts);
        }
    }
}
