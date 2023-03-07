using MediatR;
using MedokStore.Domain.Entity;
using Microsoft.AspNetCore.Identity;

namespace MedokStore.Application.Users.Command.Login
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, Guid>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInMenager;

        public LoginUserCommandHandler(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInMenager)
            => (_userManager, _signInMenager) = (userManager, signInMenager);

        public async Task<Guid> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {

            var user = await _userManager.FindByNameAsync(request.Email);
            //var result = await _signInMenager.PasswordSignInAsync(user, request.Password, false, false);

            return user.Id;
        }
    }
}
