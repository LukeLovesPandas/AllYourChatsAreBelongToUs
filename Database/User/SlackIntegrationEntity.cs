using AllYourChatsAreBelongToUs.Contracts;
namespace AllYourChatsAreBelongToUs.Database.User {
    public class SlackIntegrationEntity : ChatIntegrationEntity {

        public string ApplicationId { get; set; }

        public string SlackUserId { get; set; }

        public SlackIntegrationEntity() : base (Constants.SlackIntegrationId, "Slack") { }

    }
}