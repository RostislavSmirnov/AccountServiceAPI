using FluentValidation;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace BankAccountServiceAPI.Features.BankAccountOperations.EditBankAccount
{
    /// <summary>
    /// Класс валидации редактирования счёта
    /// </summary>
    public class EditBankAccountCommandValidator : AbstractValidator<EditBankAccountCommand>
    {
        public EditBankAccountCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("ID счета не может быть пустым.");
            RuleFor(x => x.CurrencyCodeISO)
                .NotEmpty().WithMessage("Код валюты не может быть пустым.")
                .Length(3).WithMessage("Код валюты должен состоять из 3 символов (ISO 4217).")
                .IsInEnum().WithMessage("Код валюты должен входить в список поддерживаемых валют");

            RuleFor(x => x.interestRate)
                .GreaterThan(0).When(x => x.interestRate.HasValue)
                .WithMessage("Процентная ставка должна быть больше нуля.");
        }
    }
}
