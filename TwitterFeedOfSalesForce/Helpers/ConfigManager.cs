using System.Configuration;

namespace TwitterFeedOfSalesForce.Helpers
{
    /// <summary>
    /// Helps reading from config.
    /// </summary>
    public class ConfigManager
    {
        private static ConfigManager _instance;

        private ConfigManager()
        {
            ConsumerKey = ConfigurationManager.AppSettings["TwitterConsumerKey"];
            ConsumerSecret = ConfigurationManager.AppSettings["TwitterConsumerSecret"];
            DefaultUsername = ConfigurationManager.AppSettings["DefaultUsername"];
            DefaultPassword = ConfigurationManager.AppSettings["DefaultPassword"];
        }

        public string ConsumerKey { get; set; }
        public string ConsumerSecret { get; set; }
        public string DefaultUsername { get; set; }
        public string DefaultPassword { get; set; }

        public static ConfigManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ConfigManager();
                }
                return _instance;
            }
        }
    }
}