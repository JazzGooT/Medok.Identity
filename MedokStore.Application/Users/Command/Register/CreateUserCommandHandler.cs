using MediatR;
using MedokStore.Domain.Entity;
using Microsoft.AspNetCore.Identity;

namespace MedokStore.Application.Users.Command.Register
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, IdentityResult>
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInMenager;
        public CreateUserCommandHandler(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, SignInManager<ApplicationUser> signInMenager)
            => (_userManager, _roleManager, _signInMenager) = (userManager, roleManager, signInMenager);
        public async Task<IdentityResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = new ApplicationUser
            {
                UserName = request.UserName,
                LastName = request.LastName,
                Email = request.Email,
                PasswordHash = request.Password,
                EmailConfirmed = false,

            };
            var result = await _userManager.CreateAsync(user);
            await _userManager.AddToRoleAsync(user, "Client");
            await _userManager.UpdateAsync(user);

            return result;
        }
    }
}
