using Microsoft.Extensions.DependencyInjection;

namespace RetailOrdering.Shared.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSharedServices(this IServiceCollection services)
        {
            return services;
        }
    }
}
