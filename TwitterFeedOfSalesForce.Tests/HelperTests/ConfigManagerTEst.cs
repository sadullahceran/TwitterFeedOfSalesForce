using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TwitterFeedOfSalesForce.Helpers;

namespace TwitterFeedOfSalesForce.Tests.HelperTests
{
    /// <summary>
    /// Summary description for HomeTest
    /// </summary>
    [TestClass]
    public class ConfigManagerTest
    {
        private static ConfigManager _manager;

        private TestContext testContextInstance;

        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }
        public ConfigManagerTest()
        {
        }

        [ClassInitialize]
        public static void InitializeManager(TestContext context)
        {
            _manager = ConfigManager.Instance;
        }

        [TestMethod]
        public void TestConsumerKey()
        {
            Assert.AreEqual(ConfigurationManager.AppSettings["TwitterConsumerKey"], _manager.ConsumerKey);
        }
        [TestMethod]
        public void TestConsumerSecret()
        {
            Assert.AreEqual(ConfigurationManager.AppSettings["TwitterConsumerSecret"], _manager.ConsumerSecret);
        }
        [TestMethod]
        public void TestDefaultUsername()
        {
            Assert.AreEqual(ConfigurationManager.AppSettings["DefaultUsername"], _manager.DefaultUsername);
        }
        [TestMethod]
        public void TestDefaultPassword()
        {
            Assert.AreEqual(ConfigurationManager.AppSettings["DefaultPassword"], _manager.DefaultPassword);
        }
    }
}
