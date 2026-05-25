using Microsoft.Extensions.DependencyInjection;

namespace RetailOrdering.Shared.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddSharedServices(this IServiceCollection services)
        {
            // Add any shared services/utilities here
            return services;
        }
    }
}
