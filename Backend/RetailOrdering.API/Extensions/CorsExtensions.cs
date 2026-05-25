using Microsoft.Extensions.DependencyInjection;

namespace RetailOrdering.API.Extensions
{
    public static class CorsExtensions
    {
        public static IServiceCollection AddDefaultCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });

            return services;
        }
    }
}
