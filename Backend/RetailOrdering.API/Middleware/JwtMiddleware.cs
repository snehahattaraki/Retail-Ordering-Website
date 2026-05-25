using Microsoft.AspNetCore.Http;

namespace RetailOrdering.API.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Placeholder for JWT validation logic
            await _next(context);
        }
    }
}
