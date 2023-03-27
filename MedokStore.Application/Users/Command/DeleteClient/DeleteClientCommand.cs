using MediatR;

namespace MedokStore.Application.Users.Command.DeleteClient
{
    public class DeleteClientCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
    }
}
