using Microsoft.AspNetCore.Mvc.Filters;

namespace WebRest.Controllers
{
    public class BasicAuthentication : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (string.IsNullOrEmpty(context.HttpContext.Request.Headers.Authorization))
            {
                context.Result = new Microsoft.AspNetCore.Mvc.UnauthorizedResult();
            } else {
                var authHeader = context.HttpContext.Request.Headers.Authorization.ToString();
                var authHeaderParts = authHeader.Split(' ');

                if (authHeaderParts.Length != 2 || authHeaderParts[0] != "Basic")
                {
                    context.Result = new Microsoft.AspNetCore.Mvc.UnauthorizedResult();
                    return;
                }
                var credentials = System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(authHeaderParts[1]));
                var parts = credentials.Split(':');

                if (parts.Length != 2 || parts[0].ToLower() != "Deardorff01" || parts[1] != "Password1")
                {
                    context.Result = new Microsoft.AspNetCore.Mvc.UnauthorizedResult();
                } else {
                    base.OnActionExecuting(context);
                }
            }
        }
    }
}
