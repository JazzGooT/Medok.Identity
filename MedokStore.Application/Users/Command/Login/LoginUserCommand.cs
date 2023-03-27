using MediatR;

namespace MedokStore.Application.Users.Command.Login
{
    public class LoginUserCommand : IRequest<LoginUserVm>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
