using System.Runtime.Serialization;

namespace AllYourChatsAreBelongToUs.Contracts.AYCABTUApi.Slack {
    [DataContract]
    public class SlackIntegration : ChatIntegration {

        [DataMember]
        public string ApplicationId { get; set; }
        [DataMember]
        public string SlackUserId { get; set; }
    }
}