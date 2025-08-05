using FluentValidation;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace BankAccountServiceAPI.Features.TransferOperations.MakeTransfer
{
    /// <summary>
    /// Класс валидации выполнения перевода
    /// </summary>
    public class MakeTransferCommandValidator : AbstractValidator<MakeTransferCommand>
    {
        public MakeTransferCommandValidator()
        {
            RuleFor(x => x.AccountId)
                .NotEmpty().WithMessage("ID исходного счета не может быть пустым.");

            RuleFor(x => x.CounterPartyAccountId)
                .NotEmpty().WithMessage("ID целевого счета не может быть пустым.")
                .NotEqual(x => x.AccountId).WithMessage("Исходный и целевой счета не могут быть одинаковыми.");

            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("Сумма перевода должна быть больше нуля.");
        }
    }
}
