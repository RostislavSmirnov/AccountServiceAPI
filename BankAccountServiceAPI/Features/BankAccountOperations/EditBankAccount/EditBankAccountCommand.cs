using BankAccountServiceAPI.Common;
using MediatR;

namespace BankAccountServiceAPI.Features.BankAccountOperations.EditBankAccount
{
    public class EditBankAccountCommand : IRequest<MbResult<BankAccountDto>>
    {
        /// <summary>
        /// Уникальный идентификатор счёта который необходимо изменить.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Тип валюты, на который необходимо изменить счёт, в формате из трёх букв
        /// </summary>
        public string? CurrencyCodeISO { get; set; }

        /// <summary>
        /// Процентная ставка, если тип счёта её поддерживает
        /// </summary>
        public decimal? interestRate { get; set; }
        
    }
}
