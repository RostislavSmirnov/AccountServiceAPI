using AutoMapper;
using BankAccountServiceAPI.Entities;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace BankAccountServiceAPI.Features.TransferOperations
{
    /// <summary>
    /// Маппинг необходимый лоя транзакций 
    /// </summary>
    public class TransferMappingProfile : Profile
    {
        public TransferMappingProfile()
        {
            CreateMap<Transaction , ShowTransactionDto>();
            CreateMap<ShowTransactionDto, Transaction>();
        }
    }
}
