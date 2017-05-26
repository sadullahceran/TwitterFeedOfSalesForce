using System.Web;
using LinqToTwitter;

namespace TwitterFeedOfSalesForce.Helpers
{
    public class SessionHelper
    {
        public static ICredentialStore TwitterAuth
        {
            get
            {
                if (HttpContext.Current.Session[SessionKey.TwitterAuth] != null)
                    return (ICredentialStore) HttpContext.Current.Session[SessionKey.TwitterAuth];

                return null;
            }
            set { HttpContext.Current.Session[SessionKey.TwitterAuth] = value; }
        }

        public static bool IsAuthenticated
        {
            get
            {
                if (HttpContext.Current.Session[SessionKey.IsAuthenticated] != null)
                    return (bool) HttpContext.Current.Session[SessionKey.IsAuthenticated];

                return false;
            }
            set { HttpContext.Current.Session[SessionKey.IsAuthenticated] = value; }
        }
    }
}