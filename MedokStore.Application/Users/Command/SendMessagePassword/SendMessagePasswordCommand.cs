using MediatR;

namespace MedokStore.Application.Users.Command.SendMessagePassword
{
    public class SendMessagePasswordCommand : IRequest<SendMessagePasswordVm>
    {
        public string AccessToken { get; set; }
    }
}
