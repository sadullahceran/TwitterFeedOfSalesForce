using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TwitterFeedOfSalesForce.Helpers
{
    public class AuthorizeUserAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.Session[SessionKey.IsAuthenticated] != null)
            {
                return (bool) httpContext.Session[SessionKey.IsAuthenticated];
            }
            return false;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary(
                    new
                    {
                        controller = "Login",
                        action = "Index"
                    })
                );
        }
    }
}