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
        /// <summary>
        /// Уникальный идентификатор счёта с которого совершется перевод
        /// </summary>
        public Guid AccountId { get; set; }

        /// <summary>
        /// Уникальный идентификатор счёта на который совершается перевод
        /// </summary>
        public Guid CounterPartyAccountId { get; set; } 

        /// <summary>
        /// Сумма перевода
        /// </summary>
        public decimal Amount { get; set; } 

        /// <summary>
        /// Комментарии к переводу
        /// </summary>
        public string? MetaData { get; set; }
    }
}
