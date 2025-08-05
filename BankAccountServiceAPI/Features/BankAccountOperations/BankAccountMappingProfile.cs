using AutoMapper;
using BankAccountServiceAPI.Entities;
using BankAccountServiceAPI.Features.BankAccountOperations.CreateBankAccount;
using BankAccountServiceAPI.Features.TransferOperations;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace BankAccountServiceAPI.Features.BankAccountOperations
{
    /// <summary>
    /// Профиль преобразования для счетов
    /// </summary>
    public class BankAccountMappingProfile : Profile
    {
        public BankAccountMappingProfile()
        {
            CreateMap<BankAccountDto, BankAccount>();
            CreateMap<BankAccount, BankAccountDto>();

            CreateMap<CreateBankAccountCommand, BankAccount>();
            CreateMap<BankAccount, CreateBankAccountCommand>();

            CreateMap<CreateBankAccountCommand, BankAccount>();

            CreateMap<Transaction, ShowTransactionDto>()
                .ForMember(dest => dest.TransactionType, opt => opt.MapFrom(src => src.TransactionType.ToString()));
        }
    }
}
