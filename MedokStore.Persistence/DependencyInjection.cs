using MedokStore.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace MedokStore.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration["DbConnection"];
            services.AddDbContext<MedokStoreDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            services.AddScoped<IMedokStoreDbContext>(provider => provider.GetService<MedokStoreDbContext>());
            return services;
        }
    }
}
