using System.Threading.Tasks;
using System.Web.Mvc;
using LinqToTwitter;
using TwitterFeedOfSalesForce.Helpers;

namespace TwitterFeedOfSalesForce.Controllers
{
    [AuthorizeUser]
    public class SecureController : Controller
    {
        /// <summary>
        /// This base class makes it easy for other controllers to access twitter context.
        /// </summary>
        /// <returns></returns>
        protected async Task<TwitterContext> GetTwitterContext()
        {
            IAuthorizer auth = null;

            if (SessionHelper.TwitterAuth != null)
            {
                auth = new MvcAuthorizer
                {
                    CredentialStore = new SessionStateCredentialStore()
                };
            }
            else
            {
                auth = new ApplicationOnlyAuthorizer
                {
                    CredentialStore = new InMemoryCredentialStore
                    {
                        ConsumerKey = ConfigManager.Instance.ConsumerKey,
                        ConsumerSecret = ConfigManager.Instance.ConsumerSecret
                    }
                };

                await auth.AuthorizeAsync();
            }

            return new TwitterContext(auth);
        }
    }
}