using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TwitterFeedOfSalesForce.Controllers;

namespace TwitterFeedOfSalesForce.Tests.ControllerTests
{
    /// <summary>
    /// Summary description for HomeTest
    /// </summary>
    [TestClass]
    public class HomeTest : BaseControllerTest
    {
        public HomeTest()
        {
        }

        [TestInitialize()]
        public void MyTestInitialize()
        {
            var fakeContext = FakeHttpContext();
            HttpContext.Current = fakeContext;
        }

        [TestMethod]
        public async Task Index()
        {

            var controller = new HomeController();
            var result = await controller.Index() as ViewResult;
            Assert.AreEqual("", result.ViewName);
        }
    }
}
