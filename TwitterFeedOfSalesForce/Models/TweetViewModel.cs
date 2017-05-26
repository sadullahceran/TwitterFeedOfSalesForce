using System;

namespace TwitterFeedOfSalesForce.Models
{
    public class TweetModel
    {
        public string Username { get; set; }
        public string ScreenName { get; set; }
        public string ProfileImageUrl { get; set; }
        public string Text { get; set; }
        public string ImageUrl { get; set; }
        public int RetweetCount { get; set; }
        public DateTime Timestamp { get; set; }
        public string Link { get; set; }
    }
}