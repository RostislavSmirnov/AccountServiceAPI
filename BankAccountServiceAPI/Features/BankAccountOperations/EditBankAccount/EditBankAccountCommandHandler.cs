using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using BankAccountServiceAPI.Common;
using BankAccountServiceAPI.Entities;
using BankAccountServiceAPI.Infrastructure.MockRepository;
using MediatR;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace BankAccountServiceAPI.Features.BankAccountOperations.EditBankAccount
{
    /// <summary>
    /// Класс опичывающий логику редактирования счёта
    /// </summary>
    [SuppressMessage("ReSharper", "SuggestVarOrType_SimpleTypes")] 
    public class EditBankAccountCommandHandler : IRequestHandler<EditBankAccountCommand, MbResult<BankAccountDto>>
    {
        private readonly IMapper _mapper;
        private readonly IMockBankAccountRepository _bankAccountRepository;
        // ReSharper disable once ConvertToPrimaryConstructor считаю обычный конструктор более читаемым 
        public EditBankAccountCommandHandler(IMapper mapper, IMockBankAccountRepository mockBankAccountRepository)
        {
            _mapper = mapper;
            _bankAccountRepository = mockBankAccountRepository;
        }
        public async Task<MbResult<BankAccountDto>> Handle(EditBankAccountCommand request, CancellationToken cancellationToken)
        {
            BankAccount? account = await _bankAccountRepository.GetBankAccountById(request.Id);

            if (account == null)
            {
                throw new Exception($"Счёт с ID {request.Id} не найден");
            }

            if (request.CurrencyCodeISO != null) account.CurrencyCodeISO = request.CurrencyCodeISO;
            account.InterestRate = request.interestRate;

            try
            {
                BankAccount updatedAccount = await _bankAccountRepository.EditBankAccount(account);
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