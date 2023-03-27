using MediatR;

namespace MedokStore.Application.Users.Command.SendMessageEmail
{
    public class SendMessageEmailCommand : IRequest<SendMessageEmailVm>
    {
        public string AccessToken { get; set; }
    }
}
