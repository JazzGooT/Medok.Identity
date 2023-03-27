using FluentValidation;

namespace MedokStore.Application.Users.Command.Register
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(user => user.UserName).NotEmpty();
            RuleFor(user => user.LastName).NotEmpty();
            RuleFor(user => user.Email).NotEmpty();
            RuleFor(user => user.Password).NotEmpty();
        }
    }
}
