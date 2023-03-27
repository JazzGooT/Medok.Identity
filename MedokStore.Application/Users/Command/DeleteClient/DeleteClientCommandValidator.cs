using FluentValidation;

namespace MedokStore.Application.Users.Command.DeleteClient
{
    public class DeleteClientCommandValidator : AbstractValidator<DeleteClientCommand>
    {
        public DeleteClientCommandValidator()
        {
            RuleFor(user => user.Id).NotEmpty();
        }
    }
}
