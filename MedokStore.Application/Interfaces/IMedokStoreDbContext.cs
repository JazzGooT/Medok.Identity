using MedokStore.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace MedokStore.Application.Interfaces
{
    public interface IMedokStoreDbContext
    {
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<ApplicationRole> ApplicationRole { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
