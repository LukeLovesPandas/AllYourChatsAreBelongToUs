using AllYourChatsAreBelongToUs.Contracts;
namespace AllYourChatsAreBelongToUs.Database.User {
    public class SlackIntegration : ChatIntegration {

        public string ApplicationId { get; set; }

        public string SlackUserId { get; set; }

        // public SlackUserDetails SlackUserDetails { get; set; }
        public SlackIntegration() : base (Constants.SlackIntegrationId, "Slack") { }

    }
}