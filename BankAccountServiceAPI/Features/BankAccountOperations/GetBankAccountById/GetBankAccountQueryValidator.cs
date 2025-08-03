using FluentValidation;

namespace BankAccountServiceAPI.Features.BankAccountOperations.GetBankAccountById
{
    public class GetBankAccountQueryValidator : AbstractValidator<GetBankAccountQuery>
    {
        public GetBankAccountQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();
        }
    }
}
