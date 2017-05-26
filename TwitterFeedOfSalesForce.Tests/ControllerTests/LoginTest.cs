using System.Web;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TwitterFeedOfSalesForce.Controllers;
using TwitterFeedOfSalesForce.Helpers;
using TwitterFeedOfSalesForce.Models;

namespace TwitterFeedOfSalesForce.Tests.ControllerTests
{
    /// <summary>
    /// Summary description for HomeTest
    /// </summary>
    [TestClass]
    public class LoginTest : BaseControllerTest
    {
        public LoginTest()
        {
        }

        [TestInitialize()]
        public void MyTestInitialize()
        {
            var fakeContext = FakeHttpContext();
            HttpContext.Current = fakeContext;
        }

        [TestMethod]
        public void Index()
        {
            var controller = new LoginController();
            var result = controller.Index() as ViewResult;
            Assert.AreEqual("", result.ViewName);
        }
        [TestMethod]
        public void FailedLogin()
        {
            var loginInfo = new LoginViewModel()
            {
                Username = "falseusername",
                Password = "falsepassword"
            };

            var controller = new LoginController();
            var result = controller.Index(loginInfo) as ViewResult;
            Assert.AreEqual("", result.ViewName);
        }
        [TestMethod]
        public void SuccessfulLogin()
        {
            var loginInfo = new LoginViewModel()
            {
                Username = ConfigManager.Instance.DefaultUsername,
                Password = ConfigManager.Instance.DefaultPassword
            };

            var controller = new LoginController();

            RedirectToRouteResult redirectResult =
                 (RedirectToRouteResult)controller.Index(loginInfo);

            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.AreEqual("Home", redirectResult.RouteValues["controller"]);
        }
    }
}
