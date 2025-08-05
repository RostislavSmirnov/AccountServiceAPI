using AutoMapper;
using BankAccountServiceAPI.Common;
using BankAccountServiceAPI.Infrastructure.MockRepository;
using MediatR;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace BankAccountServiceAPI.Features.BankAccountOperations.GetBankAccounts
{
    /// <summary>
    /// Обработчик комманды вывода всех счетов
    /// </summary>
    public class GetBankAccountsQueryHandler : IRequestHandler<GetBankAccountsQuery, MbResult<IEnumerable<BankAccountDto>>>
    {
        private readonly IMockBankAccountRepository _mockBankAccountRepository;
        private readonly IMapper _mapper;
        // ReSharper disable once ConvertToPrimaryConstructor
        public GetBankAccountsQueryHandler(IMockBankAccountRepository mockBankAccountRepository, IMapper mapper)
        {
            _mockBankAccountRepository = mockBankAccountRepository;
            _mapper = mapper;
        }

        public async Task<MbResult<IEnumerable<BankAccountDto>>> Handle(GetBankAccountsQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<BankAccountDto> accounts = _mapper.Map<IEnumerable<BankAccountDto>>(await _mockBankAccountRepository.GetAllBankAccounts());
            return MbResult<IEnumerable<BankAccountDto>>.Success(accounts);
        }
    }
}
