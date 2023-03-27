using MediatR;
using MedokStore.Application.Common.Helpers.Email;
using MedokStore.Domain.Entity;
using Microsoft.AspNetCore.Identity;
using OtpNet;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MedokStore.Application.Users.Command.SendMessageEmail
{
    public class SendMessageEmailCommandHandler : IRequestHandler<SendMessageEmailCommand, SendMessageEmailVm>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public SendMessageEmailCommandHandler(UserManager<ApplicationUser> userManager)
            => _userManager = userManager;
        public async Task<SendMessageEmailVm> Handle(SendMessageEmailCommand request, CancellationToken cancellationToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(request.AccessToken);
            var tokenS = handler.ReadToken(request.AccessToken) as JwtSecurityToken;

            var email = tokenS.Claims.First(claim => claim.Type == ClaimTypes.Email).Value;
            var name = tokenS.Claims.First(claim => claim.Type == ClaimTypes.Name).Value;

            var user = await _userManager.FindByNameAsync(name);
            if (!user.EmailConfirmed)
            {
                byte[] secretKey = Encoding.ASCII.GetBytes(email);
                var totp = new Totp(secretKey, mode: OtpHashMode.Sha256, step: 300, totpSize: 6);

                var code = totp.ComputeTotp(DateTime.UtcNow);
                var remainingTime = totp.RemainingSeconds();

                var s = EmailSendMessage.EmailSender(email, name, code);
                var result = new SendMessageEmailVm
                {
                    Result = "Succeeded",
                    Email = email,
                    Expires = remainingTime.ToString()
                };
                return result;
            }
            else
            {
                var result = new SendMessageEmailVm
                {
                    Result = "Email already confirmed.",
                    Email = email,
                    Expires = null
                };
                return result;
            }
        }
    }
}
