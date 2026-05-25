using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RetailOrdering.Application.Interfaces.Repositories;
using RetailOrdering.Infrastructure.Data;
using RetailOrdering.Infrastructure.Repositories;

namespace RetailOrdering.Infrastructure
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            // Register DbContext
            services.AddScoped<ApplicationDbContext>();
            
            // Register repositories
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            
            return services;
        }
    }
}
