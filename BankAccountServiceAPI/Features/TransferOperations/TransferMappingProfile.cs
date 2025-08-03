using AutoMapper;
using BankAccountServiceAPI.Entities;

namespace BankAccountServiceAPI.Features.TransferOperations
{
    public class TransferMappingProfile : Profile
    {
        public TransferMappingProfile()
        {
            CreateMap<Transaction , ShowTransactionDto>();
            CreateMap<ShowTransactionDto, Transaction>();
        }
    }
}
