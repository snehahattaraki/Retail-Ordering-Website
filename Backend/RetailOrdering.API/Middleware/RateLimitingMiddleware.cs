using Microsoft.AspNetCore.Http;

namespace RetailOrdering.API.Middleware
{
    public class RateLimitingMiddleware
    {
        private readonly RequestDelegate _next;

        public RateLimitingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Placeholder for rate limiting
            await _next(context);
        }
    }
}
