using FluentValidation;
namespace AccountServiceAPI.Features.Transactions.RegisterTransaction
{
    public class RegisterTransactionCommandValidator: AbstractValidator<RegisterTransactionCommand>
    {
        public RegisterTransactionCommandValidator()
        {
            RuleFor(x => x.AccountId).NotEmpty();
            RuleFor(x => x.Amount).GreaterThan(0);
            RuleFor(x => x.Type).IsInEnum();
        }
    }
}
