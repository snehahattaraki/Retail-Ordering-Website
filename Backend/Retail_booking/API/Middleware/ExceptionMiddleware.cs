using System.Net;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using RetailOrdering.API.Responses;

namespace RetailOrdering.API.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var resp = ApiResponse<string>.FailResponse(ex.Message);
            await context.Response.WriteAsync(JsonSerializer.Serialize(resp));
        }
    }
}
