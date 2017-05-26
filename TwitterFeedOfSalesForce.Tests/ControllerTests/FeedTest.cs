using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TwitterFeedOfSalesForce.Controllers;

namespace TwitterFeedOfSalesForce.Tests.ControllerTests
{
    /// <summary>
    /// Summary description for HomeTest
    /// </summary>
    [TestClass]
    public class FeedTest : BaseControllerTest
    {
        public FeedTest()
        {
        }

        [TestInitialize()]
        public void MyTestInitialize()
        {
            var fakeContext = FakeHttpContext();
            HttpContext.Current = fakeContext;
        }

        [TestMethod]
        public async Task ProfileInfo()
        {
            var controller = new FeedController();
            var result = await controller.ProfileInfo() as ViewResult;
            Assert.AreEqual("", result.ViewName);
        }

        [TestMethod]
        public async Task GetTweets()
        {
            var controller = new FeedController();
            var result = await controller.GetTweets() as ViewResult;
            Assert.AreEqual("", result.ViewName);
        }

        [TestMethod]
        public async Task BeginAuthorization()
        {
            var httpRequest = new HttpRequest("", "http://localhost.local/TwitterFeedOfSalesForce/Feed/BeginAuthorization", "");
            var stringWriter = new StringWriter();
            var httpResponse = new HttpResponse(stringWriter);
            var httpContext = new HttpContext(httpRequest, httpResponse);

            var sessionContainer = new HttpSessionStateContainer("id", new SessionStateItemCollection(),
                                                    new HttpStaticObjectsCollection(), 10, true,
                                                    HttpCookieMode.AutoDetect,
                                                    SessionStateMode.InProc, false);

            httpContext.Items["AspSession"] = typeof(HttpSessionState).GetConstructor(
                                        BindingFlags.NonPublic | BindingFlags.Instance,
                                        null, CallingConventions.Standard,
                                        new[] { typeof(HttpSessionStateContainer) },
                                        null)
                                .Invoke(new object[] { sessionContainer });


            HttpContext.Current = httpContext;
            var controller = new FeedController();

            var result = (RedirectResult)await controller.BeginAuthorization();
            System.Diagnostics.Debug.WriteLine(result.Url);
            Assert.IsTrue(result.Url.Contains("api.twitter.com"));
        }
    }
}
