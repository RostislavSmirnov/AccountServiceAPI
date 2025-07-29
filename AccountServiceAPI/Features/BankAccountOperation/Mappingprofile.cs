using AccountServiceAPI.Entities;
using AccountServiceAPI.Features.BankAccountModels;
using AccountServiceAPI.Features.BankAccountOperation.CreateBankAccount;
using AutoMapper;

namespace AccountServiceAPI.Features.BankAccountOperation
{
    public class Mappingprofile : Profile
    {
        public Mappingprofile()
        {
            CreateMap<CreateBankAccountCommand, BankAccount>();

            CreateMap<BankAccount, BankAccountDto>()
                .ForMember(dest => dest.AccountType, opt => opt.MapFrom(src => src.AccountType.ToString()));

            CreateMap<Transaction, TransactionDto>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.TransactionType.ToString()));
        }
    }
}