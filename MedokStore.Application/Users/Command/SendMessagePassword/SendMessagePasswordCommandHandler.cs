using MediatR;
using MedokStore.Application.Common.Helpers.Email;
using MedokStore.Domain.Entity;
using Microsoft.AspNetCore.Identity;
using OtpNet;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MedokStore.Application.Users.Command.SendMessagePassword
{
    public class SendMessagePasswordCommandHandler : IRequestHandler<SendMessagePasswordCommand, SendMessagePasswordVm>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public SendMessagePasswordCommandHandler(UserManager<ApplicationUser> userManager) => _userManager = userManager;
        public async Task<SendMessagePasswordVm> Handle(SendMessagePasswordCommand request, CancellationToken cancellationToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(request.AccessToken);
            var tokenS = handler.ReadToken(request.AccessToken) as JwtSecurityToken;

            var email = tokenS.Claims.First(claim => claim.Type == ClaimTypes.Email).Value;
            var name = tokenS.Claims.First(claim => claim.Type == ClaimTypes.Name).Value;
            var user = await _userManager.FindByNameAsync(name);
            if (user != null)
            {
                byte[] secretKey = Encoding.ASCII.GetBytes(email + name);
                var totp = new Totp(secretKey, mode: OtpHashMode.Sha256, step: 300, totpSize: 6);

                var code = totp.ComputeTotp(DateTime.UtcNow);
                var remainingTime = totp.RemainingSeconds();

                var s = EmailSendMessage.EmailSender(email, name, code);
                var result = new SendMessagePasswordVm
                {
                    Result = "Succeeded",
                    UserName = name,
                    Expires = remainingTime.ToString()
                };
                return result;
            }
            else
            {
                var result = new SendMessagePasswordVm
                {
                    Result = "User doesn't exist.",
                    UserName = name,
                    Expires = null
                };
                return result;
            }

        }
    }
}
