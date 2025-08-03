using AutoMapper;
using BankAccountServiceAPI.Common;
using BankAccountServiceAPI.Entities;
using BankAccountServiceAPI.Infrastructure.MockRepository;
using MediatR;

namespace BankAccountServiceAPI.Features.BankAccountOperations.GetBankAccountById
{
    public class GetBankAccountQueryHandler : IRequestHandler<GetBankAccountQuery, MbResult<BankAccountDto>>
    {
        private readonly IMapper _mapper;
        private readonly IMockBankAccountRepository _bankAccountRepository;
        public GetBankAccountQueryHandler(IMapper mapper, IMockBankAccountRepository bankAccountRepository)
        {
            _mapper = mapper;
            _bankAccountRepository = bankAccountRepository;
        }

        public async Task<MbResult<BankAccountDto>> Handle(GetBankAccountQuery request, CancellationToken cancellationToken)
        {
            BankAccount? bankAccount = await _bankAccountRepository.GetBankAccountById(request.Id);
            
            if (bankAccount == null)
            {
                throw new Exception($"Счёт с ID {request.Id} не найден");
            }
            
            try
            {
                var result = _mapper.Map<BankAccountDto>(bankAccount);
                return MbResult<BankAccountDto>.Success(result);
            }
            catch (Exception ex)
            {
                throw new Exception($"Произошла ошибка {ex}");
            }
        }
    }
}
