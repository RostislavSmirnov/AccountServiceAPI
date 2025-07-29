using FluentValidation;

namespace AccountServiceAPI.Features.BankAccountOperation.CreateBankAccount
{
    public class CreateBankAccountCommandValidator : AbstractValidator<CreateBankAccountCommand>
    {
        public CreateBankAccountCommandValidator()
        {
            RuleFor(x => x.OwnerId)
                .NotEmpty().WithMessage("ID владельца не может быть пустым.");

            RuleFor(x => x.AccountType)
                .IsInEnum().WithMessage("Указан недопустимый тип счета.");

            RuleFor(x => x.Currency)
                .NotEmpty().WithMessage("Код валюты не может быть пустым.")
                .Length(3).WithMessage("Код валюты должен состоять из 3 символов (ISO 4217).");

            // Правило для процентной ставки
            RuleFor(x => x.ProcentRate)
                .NotNull().When(x => x.AccountType == Entities.Enums.AccountType.Deposit || x.AccountType == Entities.Enums.AccountType.Credit)
                .WithMessage("Процентная ставка обязательна для вкладов и кредитных счетов.")
                .GreaterThan(0).When(x => x.ProcentRate.HasValue)
                .WithMessage("Процентная ставка должна быть больше нуля.");
        }
    }
}