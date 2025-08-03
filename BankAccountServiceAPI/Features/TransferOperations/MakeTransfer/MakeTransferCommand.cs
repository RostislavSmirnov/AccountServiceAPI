using BankAccountServiceAPI.Entities;
using BankAccountServiceAPI.Entities.Enums;
using MediatR;
using System.ComponentModel.DataAnnotations;
using BankAccountServiceAPI.Common;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BankAccountServiceAPI.Features.TransferOperations.MakeTransfer
{
    public class MakeTransferCommand : IRequest<MbResult<ShowTransactionDto>>
    {
        //public Guid Id { get; set; }
        public Guid AccountId { get; set; }

        public Guid CounterPartyAccountId { get; set; } // Учетная запись контрагента(Куда переводят деньги)

        public decimal Amount { get; set; } //Сумма перевода


        //public TransactionInOrOut TransactionInOrOut { get; set; }

        public string? MetaData { get; set; }
    }
}
