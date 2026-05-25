using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace RetailOrdering.API.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            context.Result = new ObjectResult(new { message = "An error occurred." }) { StatusCode = 500 };
            context.ExceptionHandled = true;
        }
    }
}
