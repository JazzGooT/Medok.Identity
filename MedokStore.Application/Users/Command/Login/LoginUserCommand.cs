using MediatR;

namespace MedokStore.Application.Users.Command.Login
{
    public class LoginUserCommand : IRequest<Guid>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
