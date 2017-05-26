using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using LinqToTwitter;
using TwitterFeedOfSalesForce.Helpers;
using TwitterFeedOfSalesForce.Models;

namespace TwitterFeedOfSalesForce.Controllers
{
    [AuthorizeUser]
    public class FeedController : SecureController
    {
        public ActionResult Index()
        {
            return new EmptyResult();
        }

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

            var twitterCallbackUrl = System.Web.HttpContext.Current.Request.Url.ToString().Replace("Begin", "Complete");
            return await auth.BeginAuthorizationAsync(new Uri(twitterCallbackUrl));
        }

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

        public async Task<ActionResult> GetTweets()
        {
            var context = await GetTwitterContext();
            var tweets =
                await
                    (from tweet in context.Status
                        where tweet.Type == StatusType.User &&
                              tweet.ScreenName == "Salesforce" &&
                              tweet.Count == 10 &&
                              tweet.TweetMode == TweetMode.Extended
                        select tweet)
                        .ToListAsync();

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

        public async Task<ActionResult> ProfileInfo()
        {
            var context = await GetTwitterContext();

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
