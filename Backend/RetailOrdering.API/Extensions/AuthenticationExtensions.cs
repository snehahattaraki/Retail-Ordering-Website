using Microsoft.Extensions.DependencyInjection;

namespace RetailOrdering.API.Extensions
{
    public static class AuthenticationExtensions
    {
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            // Configure JWT authentication
            return services;
        }
    }
}
