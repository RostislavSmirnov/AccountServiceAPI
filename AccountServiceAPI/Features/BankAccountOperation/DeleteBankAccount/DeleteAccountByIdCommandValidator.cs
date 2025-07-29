using FluentValidation;
namespace AccountServiceAPI.Features.BankAccountOperation.DeleteBankAccount
{
    public class DeleteAccountByIdCommandValidator : AbstractValidator<DeleteAccountByIdCommand>
    {
        public DeleteAccountByIdCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
