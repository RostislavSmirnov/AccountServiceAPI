using AutoMapper;
using BankAccountServiceAPI.Common;
using BankAccountServiceAPI.Entities;
using BankAccountServiceAPI.Infrastructure.MockRepository;
using MediatR;

namespace BankAccountServiceAPI.Features.BankAccountOperations.EditBankAccount
{
    public class EditBankAccountCommandHandler : IRequestHandler<EditBankAccountCommand, MbResult<BankAccountDto>>
    {
        private readonly IMapper _mapper;
        private readonly IMockBankAccountRepository _bankAcountRepository;
        public EditBankAccountCommandHandler(IMapper mapper, IMockBankAccountRepository mockBankAcountRepository)
        {
            _mapper = mapper;
            _bankAcountRepository = mockBankAcountRepository;
        }
        public async Task<MbResult<BankAccountDto>> Handle(EditBankAccountCommand request, CancellationToken cancellationToken)
        {
            BankAccount? account = await _bankAcountRepository.GetBankAccountById(request.Id);

            if (account == null)
            {
                throw new Exception($"Счёт с ID {request.Id} не найден");
            }

            account.CurrencyCodeISO = request.CurrencyCodeISO;
            account.InterestRate = request.interestRate;

            try
            {
                BankAccount updatedAccount = await _bankAcountRepository.EditBankAccount(account);
                var result = _mapper.Map<BankAccountDto>(updatedAccount);
                return MbResult<BankAccountDto>.Success(result);
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex} ошибка при попытке изменить счёт");
            }
        }
    }
}