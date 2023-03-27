using MediatR;

namespace MedokStore.Application.Users.Command.ConfirmEmail
{
    public class ConfirmEmailCommand : IRequest<ConfirmEmailVm>
    {

        public string Code { get; set; }
        public string AccessToken { get; set; }
    }
}
