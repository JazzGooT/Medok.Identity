using FluentValidation;

namespace MedokStore.Application.Users.Command.SendMessageEmail
{
    public class SendMessageEmailCommandValidator : AbstractValidator<SendMessageEmailCommand>
    {
        public SendMessageEmailCommandValidator()
        {
            RuleFor(user => user.AccessToken).NotEmpty();
        }
    }
}
