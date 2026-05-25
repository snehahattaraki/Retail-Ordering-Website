using Microsoft.Extensions.DependencyInjection;

namespace RetailOrdering.API.Extensions
{
    public static class IdentityExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services)
        {
            // Configure identity services
            return services;
        }
    }
}
