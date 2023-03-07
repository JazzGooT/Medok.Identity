using MediatR;
using Microsoft.AspNetCore.Identity;

namespace MedokStore.Application.Users.Command.Register
{
    public class CreateUserCommand : IRequest<IdentityResult>
    {
        public string UserName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
