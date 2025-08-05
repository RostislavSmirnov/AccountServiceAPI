using FluentValidation;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace BankAccountServiceAPI.Features.BankAccountOperations.GetBankAccountById
{
    /// <summary>
    /// Класс валидации выдачи класса по ID
    /// </summary>
    public class GetBankAccountQueryValidator : AbstractValidator<GetBankAccountQuery>
    {
        public GetBankAccountQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();
        }
    }
}
