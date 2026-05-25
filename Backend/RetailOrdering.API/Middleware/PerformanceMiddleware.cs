using Microsoft.AspNetCore.Http;
using System.Diagnostics;

namespace RetailOrdering.API.Middleware
{
    public class PerformanceMiddleware
    {
        private readonly RequestDelegate _next;

        public PerformanceMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var sw = Stopwatch.StartNew();
            await _next(context);
            sw.Stop();
            context.Response.Headers.Add("X-Elapsed-Milliseconds", sw.ElapsedMilliseconds.ToString());
        }
    }
}
