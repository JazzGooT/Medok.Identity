using FluentValidation;

namespace MedokStore.Application.Users.Command.ConfirmEmail
{
    public class ConfirmEmailCommandValidator : AbstractValidator<ConfirmEmailCommand>
    {
        public ConfirmEmailCommandValidator()
        {
            RuleFor(user => user.Code).NotEmpty();
            RuleFor(user => user.AccessToken).NotEmpty();
        }
    }
}
