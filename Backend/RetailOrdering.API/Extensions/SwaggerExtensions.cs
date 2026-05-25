using Microsoft.Extensions.DependencyInjection;

namespace RetailOrdering.API.Extensions
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            // Add Swagger configuration
            return services;
        }
    }
}
