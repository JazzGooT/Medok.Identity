using MediatR;
using MedokStore.Domain.Entity;
using Microsoft.AspNetCore.Identity;

namespace MedokStore.Application.Users.Queries.GetUserListByRole
{
    public class GetUserListQueryHandler : IRequestHandler<GetUserListQuery, IList<ApplicationUser>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public GetUserListQueryHandler(UserManager<ApplicationUser> userManager) => _userManager = userManager;
        public async Task<IList<ApplicationUser>> Handle(GetUserListQuery request, CancellationToken cancellationToken)
        {
            var result = await _userManager.GetUsersInRoleAsync(request.Role);
            return result;
        }
    }
}
