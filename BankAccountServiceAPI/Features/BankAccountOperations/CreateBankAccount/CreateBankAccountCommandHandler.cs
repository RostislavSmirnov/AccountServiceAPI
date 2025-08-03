using AutoMapper;
using BankAccountServiceAPI.Entities;
using BankAccountServiceAPI.Infrastructure.CurrenciesSupport;
using BankAccountServiceAPI.Infrastructure.MockCustomerVerification;
using BankAccountServiceAPI.Infrastructure.MockRepository;
using MediatR;
using BankAccountServiceAPI.Common;

namespace BankAccountServiceAPI.Features.BankAccountOperations.CreateBankAccount
{
    public class CreateBankAccountCommandHandler : IRequestHandler<CreateBankAccountCommand, MbResult<Guid>>
    {
        private readonly IMockBankAccountRepository _mockBankAccountRepository;
        private readonly IMapper _mapper;
        private readonly ICurrencyService _currencyService;
        private readonly IMockCustomerVerification _mockCustomerVerification;

        public CreateBankAccountCommandHandler(IMockBankAccountRepository mockBankAccountRepository, IMapper mapper, ICurrencyService currencyService, IMockCustomerVerification mockCustomerVerification)
        {
            _mockBankAccountRepository = mockBankAccountRepository;
            _mapper = mapper;
            _currencyService = currencyService;
            _mockCustomerVerification = mockCustomerVerification;
        }

        public async Task<MbResult<Guid>> Handle(CreateBankAccountCommand request, CancellationToken cancellationToken)
        {
            BankAccount account = _mapper.Map<BankAccount>(request);

            if (!await _currencyService.ThisCurrencyIsSupported(account.CurrencyCodeISO)) 
            {
                throw new Exception($"ошибка, не поддерживаемый тип валют {request.CurrencyCodeISO}");
            }

            if (!await _mockCustomerVerification.CustomerExistAsync(account.OwnerId))
            {
                throw new Exception($"ошибка, клиент с ID {account.OwnerId} не найден ");
            }

            try
            {
                BankAccount createdBankAccount = await _mockBankAccountRepository.CreateBankAccount(account);
                return MbResult<Guid>.Success(createdBankAccount.Id);
            }
            catch (Exception ex)
            {

                return MbResult<Guid>.Failure(new MbError("CreationFailed", $"Произошла внутренняя ошибка при создании счета."));
            }
        }
    }
}
