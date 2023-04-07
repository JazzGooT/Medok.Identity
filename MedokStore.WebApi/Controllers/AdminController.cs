using MedokStore.Application.Users.Command.DeleteClient;
using MedokStore.Application.Users.Queries.GetUserListByRole;
using MedokStore.Domain.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedokStore.Identity.Controllers
{
    [Authorize(Policy = "Admin")]
    [Route("/[controller]")]
    public class AdminController : BaseController
    {
        [HttpGet("Users")]
        public async Task<IList<ApplicationUser>> Get(string Role)
        {
            var query = new GetUserListQuery
            {
                Role = Role
            };
            var result = await Mediator.Send(query);
            return result;
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteClient(Guid Id)
        {
            var command = new DeleteClientCommand
            {
                Id = Id,
            };
            await Mediator.Send(command);
            return Ok("Client deleted!");
        }
    }
}
