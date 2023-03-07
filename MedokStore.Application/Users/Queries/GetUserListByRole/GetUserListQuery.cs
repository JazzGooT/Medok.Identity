using MediatR;
using MedokStore.Domain.Entity;

namespace MedokStore.Application.Users.Queries.GetUserListByRole
{
    public class GetUserListQuery : IRequest<IList<ApplicationUser>>
    {
        public string Role { get; set; }
    }
}
