using MedokStore.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedokStore.Persistence.EntityTypeConfigurations
{
    public class ApplicationRoleConfigurations : IEntityTypeConfiguration<ApplicationRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationRole> builder)
        {

        }
    }
}
