using AccountServiceAPI.Features.BankAccountModels;
using AccountServiceAPI.Infrastructure.Persistence;
using AutoMapper;
using MediatR;

namespace AccountServiceAPI.Features.BankAccountOperation.GetBankAccountById
{
    public class GetAccountByIdHandler : IRequestHandler<GetAccountByIdQuery, BankAccountDto>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public GetAccountByIdHandler(IAccountRepository accountRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        public async Task<BankAccountDto> Handle(GetAccountByIdQuery request, CancellationToken cancellationToken)
        {
            var account = await _accountRepository.GetAccountByIdAsync(request.Id);

            if (account == null)
            {
                throw new Exception($"Счет с ID {request.Id} не найден.");
            }

            return _mapper.Map<BankAccountDto>(account);
        }
    }
}