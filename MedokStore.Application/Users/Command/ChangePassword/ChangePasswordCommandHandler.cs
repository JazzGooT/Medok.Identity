using MediatR;
using MedokStore.Domain.Entity;
using Microsoft.AspNetCore.Identity;
using OtpNet;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MedokStore.Application.Users.Command.ChangePassword
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, ChangePasswordVm>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public ChangePasswordCommandHandler(UserManager<ApplicationUser> userManager)
            => _userManager = userManager;
        public async Task<ChangePasswordVm> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(request.AccessToken);
            var tokenS = handler.ReadToken(request.AccessToken) as JwtSecurityToken;

            var email = tokenS.Claims.First(claim => claim.Type == ClaimTypes.Email).Value;
            var name = tokenS.Claims.First(claim => claim.Type == ClaimTypes.Name).Value;

            byte[] secretKey = Encoding.ASCII.GetBytes(email + name);
            var totp = new Totp(secretKey, mode: OtpHashMode.Sha256, step: 300, totpSize: 6);
            long timeStepMatched;

            var user = await _userManager.FindByNameAsync(name);

            if (user != null && totp.VerifyTotp(request.Code, out timeStepMatched, window: null) == true)
            {
                var changePassworResult = await _userManager.ChangePasswordAsync(user, request.Password, request.NewPassword);
                if (changePassworResult.Succeeded)
                {
                    var result = new ChangePasswordVm
                    {
                        Result = "Password changed.",
                    };
                    return result;
                }
                else
                {
                    var result = new ChangePasswordVm
                    {
                        Result = "Wrong password.",
                    };
                    return result;
                }
            }
            else
            {
                var result = new ChangePasswordVm
                {
                    Result = "User doesn't exist.",
                };
                return result;
            }

        }
    }
}
