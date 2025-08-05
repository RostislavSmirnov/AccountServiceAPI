using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using BankAccountServiceAPI.Common;
using BankAccountServiceAPI.Entities;
using BankAccountServiceAPI.Infrastructure.MockRepository;
using MediatR;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace BankAccountServiceAPI.Features.BankAccountOperations.GetBankAccountById
{
    /// <summary>
    /// Класс описывающий логику выдачи счёта по ID
    /// </summary>
    [SuppressMessage("ReSharper", "SuggestVarOrType_SimpleTypes")]
    public class GetBankAccountQueryHandler : IRequestHandler<GetBankAccountQuery, MbResult<BankAccountDto>>
    {
        private readonly IMapper _mapper;
        private readonly IMockBankAccountRepository _bankAccountRepository;
        // ReSharper disable once ConvertToPrimaryConstructor Считаю обычный конструктор более читаемым
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
