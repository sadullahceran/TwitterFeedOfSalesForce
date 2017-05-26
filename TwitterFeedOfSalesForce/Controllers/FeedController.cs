using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using LinqToTwitter;
using TwitterFeedOfSalesForce.Helpers;
using TwitterFeedOfSalesForce.Models;

namespace TwitterFeedOfSalesForce.Controllers
{
    /// <summary>
    /// Feed controller is responsible for Twitter profile info and twitter status update retrievals.
    /// </summary>
    [AuthorizeUser]
    public class FeedController : SecureController
    {
        /// <summary>
        /// BeginAuthorization method first reads consumer key & secret from config file, and redirects
        /// User to Twitter for authorization.
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> BeginAuthorization()
        {
            var twitterConfiguration = ConfigManager.Instance;
            var auth = new MvcAuthorizer
            {
                CredentialStore = new SessionStateCredentialStore
                {
                    ConsumerKey = twitterConfiguration.ConsumerKey,
                    ConsumerSecret = twitterConfiguration.ConsumerSecret
                }
            };

            // Callback URL is CompleteAuthorization.
            var twitterCallbackUrl = System.Web.HttpContext.Current.Request.Url.ToString().Replace("Begin", "Complete");
            return await auth.BeginAuthorizationAsync(new Uri(twitterCallbackUrl));
        }

        /// <summary>
        /// After user enters their credentials and allows our application to access their data,
        /// Twitter redirects the browser to this method and supplies it with access token.
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> CompleteAuthorization()
        {
            var auth = new MvcAuthorizer
            {
                CredentialStore = new SessionStateCredentialStore()
            };

            await auth.CompleteAuthorizeAsync(Request.Url);
            SessionHelper.TwitterAuth = auth.CredentialStore;

            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Returns last 10 tweets of Salesforce account. 
        /// @TODO Parametrize screen name, so that the same action could be reused for different
        /// screen names.
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> GetTweets()
        {
            var context = await GetTwitterContext();

            //Get last 10 tweets.
            var tweets =
                await
                    (from tweet in context.Status
                        where tweet.Type == StatusType.User &&
                              tweet.ScreenName == "Salesforce" &&
                              tweet.Count == 10 &&
                              tweet.TweetMode == TweetMode.Extended
                        select tweet)
                        .ToListAsync();

            // Convert tweets to Custom model to display.
            var tweetModels =
                (from t in tweets
                    select new TweetModel
                    {
                        Username = t.User.Name,
                        ScreenName = t.ScreenName,
                        ProfileImageUrl = t.User.ProfileImageUrl,
                        Text = t.FullText,
                        RetweetCount = t.RetweetCount,
                        Timestamp = t.CreatedAt,
                        Link = t.User.Url,
                        ImageUrl = t.Entities.MediaEntities.Select(m => m.MediaUrl).FirstOrDefault(m => m != null)
                    }).ToList();

            return View(tweetModels);
        }

        /// <summary>
        /// Retrieves profile information of Salesforce account. 
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> ProfileInfo()
        {
            var context = await GetTwitterContext();

            // Get details of salesforce account.
            var userInfoList =
                await (
                    from user in context.User
                    where user.ScreenName == "Salesforce" &&
                          user.Type == UserType.Show
                    select user).ToListAsync();

            var userInfo = userInfoList.FirstOrDefault();

            if (userInfo == null)
            {
                return View((object) null);
            }

            // Convert profile information to custom model to be displayed
            var model = new UserModel
            {
                ProfileImage = userInfo.ProfileImageUrl,
                Description = userInfo.Description,
                ScreenName = userInfo.ScreenName,
                TweetCount = userInfo.StatusesCount,
                FollowerCount = userInfo.FollowersCount,
                FollowingCount = userInfo.FriendsCount,
                Link = userInfo.Url
            };
            return View(model);
        }
    }
}
