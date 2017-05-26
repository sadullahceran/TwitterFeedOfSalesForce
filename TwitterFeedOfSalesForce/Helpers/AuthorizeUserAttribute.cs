using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TwitterFeedOfSalesForce.Helpers
{
    /// <summary>
    /// This attribute is used to secure controllers and actions.
    /// Only logged in users will be accepted by this.
    /// </summary>
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

        /// <summary>
        /// If user not authorized, redirect them to Login page.
        /// </summary>
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