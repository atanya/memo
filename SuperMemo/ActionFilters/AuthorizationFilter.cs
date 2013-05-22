using System.Web.Mvc;
using System.Web.Routing;
using SuperMemo.BL;

namespace SuperMemo.ActionFilters
{
    public class AuthorizationFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var sessionCookie = filterContext.HttpContext.Request.Cookies.Get("SuperMemoSession");

            var userService = new UserService();
            if (sessionCookie == null || userService.FindByHash(sessionCookie.Value) == null)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary {{"controller", "Authorization"}, {"action", "Login"}});
            }
        }
    }
}