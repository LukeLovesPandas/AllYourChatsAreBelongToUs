using System.Runtime.Serialization;

namespace AllYourChatsAreBelongToUs.Contracts.ViewModels.Slack {
    [DataContract]
    public class SlackIntegration : ChatIntegration {

        [DataMember]
        public string SlackToken { get; set; }
        [DataMember]
        public string SlackUserId { get; set; }
    }
}