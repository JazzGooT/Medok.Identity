using MediatR;
using MedokStore.Application.Common.Helpers;
using MedokStore.Domain.Entity;
using Microsoft.AspNetCore.Identity;

namespace MedokStore.Application.Users.Command.Login
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserVm>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInMenager;

        public LoginUserCommandHandler(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInMenager)
            => (_userManager, _signInMenager) = (userManager, signInMenager);

        public async Task<LoginUserVm> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
            {
                var result = new LoginUserVm
                {
                    Result = "Wrong login or password.",
                };
                return result;
            }
            else
            {
                var resultSignIn = await _signInMenager.CheckPasswordSignInAsync(user, request.Password, false);
                if (resultSignIn.Succeeded)
                {
                    var role = await _userManager.GetRolesAsync(user);
                    var token = TokenManager.GenerateToken(user, role[0]);
                    var result = new LoginUserVm
                    {
                        Result = resultSignIn.Succeeded ? "Succeeded" : "Failed",
                        AccessToken = token.Token,
                        Expires = token.Expires
                    };
                    return result;
                }
                else
                {
                    var result = new LoginUserVm
                    {
                        Result = "Wrong login or password.",
                    };
                    return result;
                }
            }
        }
    }
}
