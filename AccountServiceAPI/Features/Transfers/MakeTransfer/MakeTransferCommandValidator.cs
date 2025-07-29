using FluentValidation;

namespace AccountServiceAPI.Features.Transfers.MakeTransfer
{
    public class MakeTransferCommandValidator : AbstractValidator<MakeTransferCommand>
    {
        public MakeTransferCommandValidator()
        {
            RuleFor(x => x.FromAccountId).NotEmpty();
            RuleFor(x => x.ToAccountId).NotEmpty().NotEqual(x => x.FromAccountId); // Нельзя переводить на тот же счет
            RuleFor(x => x.Amount).GreaterThan(0);
        }
    }
}
