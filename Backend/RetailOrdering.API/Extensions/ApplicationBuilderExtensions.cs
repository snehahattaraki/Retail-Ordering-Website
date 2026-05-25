using Microsoft.AspNetCore.Builder;

namespace RetailOrdering.API.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseApiMiddlewares(this IApplicationBuilder app)
        {
            app.UseMiddleware<Middleware.ExceptionMiddleware>();
            app.UseMiddleware<Middleware.RequestLoggingMiddleware>();
            app.UseMiddleware<Middleware.PerformanceMiddleware>();
            app.UseMiddleware<Middleware.RateLimitingMiddleware>();
            app.UseMiddleware<Middleware.JwtMiddleware>();
            return app;
        }
    }
}
