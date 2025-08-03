using AutoMapper;
using BankAccountServiceAPI.Entities;
using BankAccountServiceAPI.Features.BankAccountOperations.CreateBankAccount;
using BankAccountServiceAPI.Features.BankAccountOperations.EditBankAccount;
using BankAccountServiceAPI.Features.TransferOperations;

namespace BankAccountServiceAPI.Features.BankAccountOperations
{
    public class BankAccountMappingProfile : Profile
    {
        public BankAccountMappingProfile()
        {
            CreateMap<BankAccountDto, BankAccount>();
            CreateMap<BankAccount, BankAccountDto>();

            CreateMap<CreateBankAccountCommand, BankAccount>();
            CreateMap<BankAccount, CreateBankAccountCommand>();

            // Маппинг для создания счета
            CreateMap<CreateBankAccountCommand, BankAccount>();

            CreateMap<Transaction, ShowTransactionDto>()
                .ForMember(dest => dest.TransactionType, opt => opt.MapFrom(src => src.TransactionType.ToString()));
        }
    }
}
