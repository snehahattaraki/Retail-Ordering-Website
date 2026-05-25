using Microsoft.AspNetCore.Mvc;

namespace RetailOrdering.API.Helpers
{
    public static class ResponseHelper
    {
        public static IActionResult Success(object data) => new OkObjectResult(data);
        public static IActionResult Error(string message, int status = 400) => new ObjectResult(new { message }) { StatusCode = status };
    }
}
