using Microsoft.AspNetCore.Identity;

namespace MedokStore.Domain.Entity
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string LastName { get; set; }
    }
}
