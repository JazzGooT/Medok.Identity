using FluentValidation;

namespace MedokStore.Application.Users.Command.Login
{
    public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
    {
        public LoginUserCommandValidator()
        {
            RuleFor(user => user.UserName).NotEmpty();
            RuleFor(user => user.Password).NotEmpty();
        }
    }
}
