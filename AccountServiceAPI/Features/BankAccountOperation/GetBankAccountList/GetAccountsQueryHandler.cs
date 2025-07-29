using AccountServiceAPI.Features.BankAccountModels;
using AccountServiceAPI.Infrastructure.Persistence;
using AutoMapper;
using MediatR;

namespace AccountServiceAPI.Features.BankAccountOperation.GetBankAccountList
{
    public class GetAccountsQueryHandler : IRequestHandler<GetAccountsFromQuery, IEnumerable<BankAccountDto>>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public GetAccountsQueryHandler(IAccountRepository accountRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BankAccountDto>> Handle(GetAccountsFromQuery request, CancellationToken cancellationToken)
        {
            var accounts = await _accountRepository.GetAllAccountsAsync();
            return _mapper.Map<IEnumerable<BankAccountDto>>(accounts);
        }
    }
}