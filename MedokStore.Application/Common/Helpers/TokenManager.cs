using MedokStore.Domain.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MedokStore.Application.Common.Helpers
{
    public class TokenManager
    {
        /// <summary>
        /// The method for creating an access token takes an instance of the class and
        /// returns a string containing an access token that is valid for 8 hours
        /// </summary>
        /// <param name="client">An instance of the class ClientsTable contains the following fields: Email Name, Surname, Password, Role</param>
        /// <returns></returns>
        public static TokenVm GenerateToken(ApplicationUser user, string role)
        {
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            var configuration = configurationBuilder.Build();
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim(ClaimTypes.Role, role)
            };


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddHours(8),
                signingCredentials: creds);
            var result = new TokenVm
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expires = token.ValidTo.ToString(),
            };
            return result;
        }
    }
    public class TokenVm
    {
        public string Token { get; set; }
        public string Expires { get; set; }
    }
}
