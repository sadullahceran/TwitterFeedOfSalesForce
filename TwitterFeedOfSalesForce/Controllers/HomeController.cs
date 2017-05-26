using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using LinqToTwitter;
using TwitterFeedOfSalesForce.Helpers;
using TwitterFeedOfSalesForce.Models;

namespace TwitterFeedOfSalesForce.Controllers
{
    [AuthorizeUser]
    public class HomeController : SecureController
    {
        //
        // GET: /Home/

        /// <summary>
        /// Main page. If user authorized the app, their profile info will be shown.
        /// </summary>
        /// <returns></returns>
        public async Task<ViewResult> Index()
        {
            if (SessionHelper.TwitterAuth == null)
            {
                return View((object) null);
            }

            // Get twitter user id
            var userId = SessionHelper.TwitterAuth.UserID;

            var context = await GetTwitterContext();

            // Get twitter user detail
            var userInfoList =
                await (
                    from user in context.User
                    where user.UserID == userId &&
                          user.Type == UserType.Show
                    select user).ToListAsync();

            var userInfo = userInfoList.FirstOrDefault();

            if (userInfo == null)
            {
                return View((object) null);
            }
            // Convert twitter user detail to custom model.
            // @TODO: FeedController uses the same code, refactoring should be made.
            var model = new UserModel
            {
                ProfileImage = userInfo.ProfileImageUrl,
                Description = userInfo.Description,
                ScreenName = userInfo.ScreenNameResponse,
                FullName = userInfo.Name,
                TweetCount = userInfo.StatusesCount,
                FollowerCount = userInfo.FollowersCount,
                FollowingCount = userInfo.FriendsCount,
                Link = userInfo.Url
            };
            return View(model);
        }
    }
}