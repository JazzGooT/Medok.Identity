using MedokStore.Domain.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MedokStore.Identity.Controllers
{
    [Route("[controller]")]
    public class HomeController : BaseController
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;


        public HomeController(RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager)
            => (_roleManager, _userManager) = (roleManager, userManager);

        [HttpPost("Admin")]
        public async Task<IActionResult> CreateSAdmin()
        {
            var admin = new ApplicationUser
            {
                UserName = "SuperAdmin",
                LastName = "MedokStore",
                Email = "MedokStore@gmail.com",
                EmailConfirmed = true,
                PhoneNumber = "1234567890",
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
            };
            var result = await _userManager.CreateAsync(admin, "Admin123");

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            // Add the role to the user
            var roleResult = await _userManager.AddToRoleAsync(admin, "SuperAdmin");
            if (!roleResult.Succeeded)
            {
                return BadRequest(roleResult.Errors);
            }
            await _userManager.UpdateAsync(admin);
            return Ok("User created!");
        }
        [HttpPost("Roles")]
        public async Task<IActionResult> CreateRoles()
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

        [Authorize(Policy = "Client")]
        [HttpPost("ClientAuth")]
        public IActionResult TestUser()
        {
            return Ok("You are Client");
        }
        [Authorize(Policy = "Admin")]
        [HttpPost("AdminAuth")]
        public IActionResult TestAdmin()
        {
            return Ok("You are Admin");
        }
        [Authorize(Policy = "SuperAdmin")]
        [HttpPost("SuperAdminAuth")]
        public IActionResult TestSAdmin()
        {
            return Ok("You are SuperAdmin");
        }
    }

}
