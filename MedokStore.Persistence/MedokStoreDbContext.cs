using MedokStore.Application.Interfaces;
using MedokStore.Domain.Entity;
using MedokStore.Persistence.EntityTypeConfigurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MedokStore.Persistence
{
    public class MedokStoreDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>, IMedokStoreDbContext
    {
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<ApplicationRole> ApplicationRole { get; set; }
        public MedokStoreDbContext(DbContextOptions<MedokStoreDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ApplicationUserConfigurations());
            base.OnModelCreating(builder);
        }
    }
}
