using MedokStore.Application.Users.Queries.GetUserListByRole;
using MedokStore.Domain.Entity;
using Microsoft.AspNetCore.Mvc;

namespace MedokStore.Identity.Controllers
{
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
    }
}
