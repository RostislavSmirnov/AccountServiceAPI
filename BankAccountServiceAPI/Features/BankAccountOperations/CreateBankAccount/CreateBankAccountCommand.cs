using BankAccountServiceAPI.Entities.Enums;
using MediatR;
using BankAccountServiceAPI.Common;

namespace BankAccountServiceAPI.Features.BankAccountOperations.CreateBankAccount
{
    public class CreateBankAccountCommand : IRequest<MbResult<Guid>>
    {
        /// <summary>
        /// Уникальный идентификатор валдельца счёта
        /// </summary>
        public Guid OwnerId { get; set; } 

        /// <summary>
        /// Тип счёта, может быть Checking(Обычный), Credit(Кредитный счёт), Deposit (Накопительный счёт).
        /// </summary>
        public AccountType AccountType { get; set; }

        /// <summary>
        /// Валюта счёта, указывается в формате из трёх букв, например RUB
        /// </summary>
        public string CurrencyCodeISO { get; set; }

        /// <summary>
        /// процентная ставка, если тип счёта её поддерживает.
        /// </summary>
        public decimal? InterestRate { get; set; } 
    }
}
