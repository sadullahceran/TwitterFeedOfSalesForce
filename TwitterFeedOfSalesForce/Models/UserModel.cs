namespace TwitterFeedOfSalesForce.Models
{
    public class UserModel
    {
        public string ProfileImage { get; set; }
        public string ScreenName { get; set; }
        public int TweetCount { get; set; }
        public int FollowerCount { get; set; }
        public string Description { get; set; }
        public int FollowingCount { get; set; }
        public string Link { get; set; }
        public string FullName { get; set; }
    }
}