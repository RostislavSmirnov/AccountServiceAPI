using FluentValidation;

namespace BankAccountServiceAPI.Features.BankAccountOperations.EditBankAccount
{
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
            //RuleFor(x => x.ClosedDate)
            //    .GreaterThanOrEqualTo(DateTime.UtcNow.Date).When(x => x.ClosedDate.HasValue)
            //    .WithMessage("Дата закрытия не может быть в прошлом.");
        }
    }
}
