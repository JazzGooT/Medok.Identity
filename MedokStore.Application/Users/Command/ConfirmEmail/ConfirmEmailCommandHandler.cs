using MediatR;
using MedokStore.Domain.Entity;
using Microsoft.AspNetCore.Identity;
using OtpNet;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MedokStore.Application.Users.Command.ConfirmEmail
{
    public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, ConfirmEmailVm>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public ConfirmEmailCommandHandler(UserManager<ApplicationUser> userManager) => _userManager = userManager;
        public async Task<ConfirmEmailVm> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(request.AccessToken);
            var tokenS = handler.ReadToken(request.AccessToken) as JwtSecurityToken;

            var email = tokenS.Claims.First(claim => claim.Type == ClaimTypes.Email).Value;
            var name = tokenS.Claims.First(claim => claim.Type == ClaimTypes.Name).Value;
            var user = await _userManager.FindByEmailAsync(email);

            byte[] secretKey = Encoding.ASCII.GetBytes(email);
            var totp = new Totp(secretKey, mode: OtpHashMode.Sha256, step: 300, totpSize: 6);
            long timeStepMatched;

            if (user != null && totp.VerifyTotp(request.Code, out timeStepMatched, window: null) == true)
            {
                user.EmailConfirmed = true;
                var resultUpdate = await _userManager.UpdateAsync(user);
                if (resultUpdate.Succeeded)
                {
                    var result = new ConfirmEmailVm
                    {
                        Result = "Email confirmed",
                        Email = email,
                    };
                    return result;
                }
                else
                {
                    var result = new ConfirmEmailVm
                    {
                        Result = "Failed",
                        Email = email,
                    };
                    return result;
                }
            }
            else
            {
                var result = new ConfirmEmailVm
                {
                    Result = "Failed",
                    Email = email,
                };
                return result;
            }
        }
    }
}
