using FluentValidation;

namespace MedokStore.Application.Users.Command.SendMessagePassword
{
    public class SendMessagePasswordCommandValidator : AbstractValidator<SendMessagePasswordCommand>
    {
        public SendMessagePasswordCommandValidator()
        {
            RuleFor(user => user.AccessToken).NotEmpty();
        }
    }
}
