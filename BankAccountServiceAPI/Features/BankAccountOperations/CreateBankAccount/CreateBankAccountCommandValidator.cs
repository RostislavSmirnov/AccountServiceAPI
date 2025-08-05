using BankAccountServiceAPI.Entities.Enums;
using FluentValidation;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace BankAccountServiceAPI.Features.BankAccountOperations.CreateBankAccount
{
    /// <summary>
    /// Класс валидации создания счёта
    /// </summary>
    public class CreateBankAccountCommandValidator : AbstractValidator<CreateBankAccountCommand>
    {
        public CreateBankAccountCommandValidator()
        {
            RuleFor(x => x.OwnerId)
                .NotEmpty().WithMessage("ID Владельца счёта не может быть пустым.");

            RuleFor(x => x.AccountType)
                .IsInEnum().WithMessage("Указан недопустимый тип счёта.");

            RuleFor(x => x.CurrencyCodeISO)
                .NotEmpty().WithMessage("Код валюты не может быть пустым.")
                .Length(3).WithMessage("Код валюты должен состоять из 3 символов (ISO 4217).");

            // --- Блок правил для счетов, где ПРОЦЕНТНАЯ СТАВКА ОБЯЗАТЕЛЬНА ---
            When(x => x.AccountType == AccountType.Deposit || x.AccountType == AccountType.Credit, () =>
            {
                RuleFor(x => x.InterestRate)
                    // Правило 1: Ставка должна быть указана (не null).
                    .NotNull().WithMessage("Процентная ставка обязательна для вкладов и кредитных счетов.")
                    // Правило 2: Если она указана, она должна быть больше нуля.
                    // Знак "!" (null-forgiving operator) здесь нужен, чтобы подсказать компилятору,
                    // что после проверки NotNull() значение точно не будет null.
                    .GreaterThan(0)
                    .WithMessage("Процентная ставка для вкладов и кредитных счетов должна быть больше нуля.");
            });

            // --- Блок правил для счетов, где ПРОЦЕНТНАЯ СТАВКА ЗАПРЕЩЕНА ---
            When(x => x.AccountType == AccountType.Checking, () =>
            {
                RuleFor(x => x.InterestRate)
                    // Правило: Ставка должна отсутствовать (быть null).
                    .Null().WithMessage(
                        "Процентная ставка не применима для текущего (Checking) счета и должна отсутствовать.");
            });
        }
    }
}
