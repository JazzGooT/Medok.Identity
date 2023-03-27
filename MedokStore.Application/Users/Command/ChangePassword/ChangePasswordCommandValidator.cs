using FluentValidation;

namespace MedokStore.Application.Users.Command.ChangePassword
{
    public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordCommandValidator()
        {
            RuleFor(user => user.Code).NotEmpty();
            RuleFor(user => user.Password).NotEmpty();
            RuleFor(user => user.AccessToken).NotEmpty();
        }
    }
}
