using MedokStore.Domain.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MedokStore.Identity.Controllers
{
    [Route("[controller]")]
    public class HomeController : BaseController
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager) => (_roleManager, _userManager)
            = (roleManager, userManager);

        [HttpPost("Admin")]
        public async Task<IActionResult> CreteSAdmin()
        {
            var admin = new ApplicationUser
            {
                UserName = "Admin",
                LastName = "Super",
                Email = "Admin@gmail.com",
                PasswordHash = "Admin",
                EmailConfirmed = true,
                TwoFactorEnabled = true,
                PhoneNumber = "000000000",
                PhoneNumberConfirmed = true,
            };
            await _userManager.CreateAsync(admin);
            await _userManager.AddToRoleAsync(admin, "SuperAdmin");
            await _userManager.UpdateAsync(admin);
            return Ok("User created!");

        }
        [HttpPost("Roles")]
        public async Task<IActionResult> CreteRoles()
        {
            var role = new ApplicationRole
            {
                Name = "SuperAdmin"
            };
            await _roleManager.CreateAsync(role);
            role = new ApplicationRole
            {
                Name = "Admin"
            };
            await _roleManager.CreateAsync(role);
            role = new ApplicationRole
            {
                Name = "Client"
            };
            await _roleManager.CreateAsync(role);


            return Ok("Roles created!");
        }
        [HttpPost("Test")]
        public async Task<IActionResult> Test()
        {

            return Ok("Test Method!");
        }
    }
}
