using AccountServiceAPI.Entities;
using AccountServiceAPI.Infrastructure.Currencies;
using AccountServiceAPI.Infrastructure.CustomerVerification;
using AccountServiceAPI.Infrastructure.Persistence;
using AutoMapper;
using MediatR;
using AccountServiceAPI.Infrastructure.ValidationBehavior.Exceptions;

namespace AccountServiceAPI.Features.BankAccountOperation.CreateBankAccount
{
    public class CreatedBankAccountCommandHandler : IRequestHandler<CreateBankAccountCommand, Guid>
    {
        private readonly IMapper _mapper;
        private readonly IAccountRepository _accountRepository;
        private readonly ICustomerVerificationService _customerVerificationService;
        private readonly ICurrencyService _currencyService;

        public CreatedBankAccountCommandHandler(IMapper mapper, IAccountRepository accountRepository, ICurrencyService currencyService, ICustomerVerificationService customerVerificationService)
        {
            _mapper = mapper;
            _accountRepository = accountRepository;
            _currencyService = currencyService;
            _customerVerificationService = customerVerificationService;
        }

        public async Task<Guid> Handle(CreateBankAccountCommand request, CancellationToken cancellationToken)
        {
            if (!await _customerVerificationService.CustomerExistsAsync(request.OwnerId))
            {
                throw new NotFoundException($"Клиент с ID {request.OwnerId} не найден.");
            }
            if (!await _currencyService.IsCurrencySupportedAsync(request.Currency))
            {
                throw new BadRequestException($"Валюта '{request.Currency}' не поддерживается.");
            }
            BankAccount bankAccount = _mapper.Map<BankAccount>(request);
            var createdAccount = await _accountRepository.CreateAccountAsync(bankAccount);
            return createdAccount.Id;
        }
    }
}