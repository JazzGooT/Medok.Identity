using MediatR;

namespace MedokStore.Application.Users.Command.ChangePassword
{
    public class ChangePasswordCommand : IRequest<ChangePasswordVm>
    {
        public string Code { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
        public string AccessToken { get; set; }
    }
}
