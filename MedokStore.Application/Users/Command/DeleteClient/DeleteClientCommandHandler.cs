using MediatR;
using MedokStore.Domain.Entity;
using Microsoft.AspNetCore.Identity;

namespace MedokStore.Application.Users.Command.DeleteClient
{
    public class DeleteClientCommandHandler : IRequestHandler<DeleteClientCommand, Guid>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public DeleteClientCommandHandler(UserManager<ApplicationUser> userManager)
            => _userManager = userManager;
        public async Task<Guid> Handle(DeleteClientCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.Id.ToString());
            var role = await _userManager.GetRolesAsync(user);
            if (role.Contains("Client"))
            {
                await _userManager.DeleteAsync(user);
                return Guid.Empty;
            }
            return Guid.Empty;
        }
    }
}
