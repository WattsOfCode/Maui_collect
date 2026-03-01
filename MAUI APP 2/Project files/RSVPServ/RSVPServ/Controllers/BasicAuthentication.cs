using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RSVPServ.Models;
using System.Text;

namespace RSVPServ.Controllers
{
    public class BasicAuthentication : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (string.IsNullOrEmpty(context.HttpContext.Request.Headers.Authorization))
            { context.Result = new UnauthorizedResult(); return; }

            try
            {
                var authHeader = context.HttpContext.Request.Headers.Authorization.ToString();
                var authHeaderParts = authHeader.Split(' ');

                if (authHeaderParts.Length != 2 || authHeaderParts[0] != "Basic")
                { context.Result = new UnauthorizedResult(); return; }

                var credentials = Encoding.UTF8.GetString(Convert.FromBase64String(authHeaderParts[1]));
                var parts = credentials.Split(':');
                var username = parts[0];
                var password = parts[1];

                bool isAdmin = (username == "Deardorff01" && password == "Password1");
                bool isRegisteredUser = UserController.UserExists(username, password);

                if (isAdmin || isRegisteredUser)
                {
                    base.OnActionExecuting(context);
                }
                else
                {
                    context.Result = new UnauthorizedResult();
                }
            }
            catch
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}
