using Microsoft.Extensions.DependencyInjection;

namespace RetailOrdering.API.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Register application services here
            return services;
        }
    }
}
