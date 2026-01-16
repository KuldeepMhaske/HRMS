using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HRMS.Filters
{
    public class AuthorizeRoleAttribute : ActionFilterAttribute
    {
        private readonly string _role;

        public AuthorizeRoleAttribute(string role)
        {
            _role = role;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var session = context.HttpContext.Session;

            // Not logged in
            if (string.IsNullOrEmpty(session.GetString("AuthType")))
            {
                context.Result = new RedirectToActionResult("Login", "Account", null);
                return;
            }

            // Role mismatch
            if (session.GetString("AuthType") != _role)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            base.OnActionExecuting(context);
        }
    }
}
