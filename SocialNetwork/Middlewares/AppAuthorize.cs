using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SocialNetwork.Controllers;

namespace SocialNetwork.Middlewares
{
    public class AppAuthorize : IAsyncActionFilter
    {
        private readonly ValidateUserSession _userSession;

        public AppAuthorize(ValidateUserSession userSession)
        {
            _userSession = userSession;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (_userSession.HasUser())
            {
                await next();
            }
            else
            {
                context.Result = new RedirectToActionResult("Login", "User", null);
            }
        }
    }
}
