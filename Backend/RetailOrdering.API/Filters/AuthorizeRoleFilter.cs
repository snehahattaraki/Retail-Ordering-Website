using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace RetailOrdering.API.Filters
{
    public class AuthorizeRoleFilter : IAuthorizationFilter
    {
        private readonly string _role;

        public AuthorizeRoleFilter(string role)
        {
            _role = role;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // Placeholder: implement role-based auth check
            // If not authorized:
            // context.Result = new ForbidResult();
        }
    }
}
