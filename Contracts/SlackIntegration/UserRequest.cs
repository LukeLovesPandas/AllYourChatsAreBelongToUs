using System.Runtime.Serialization;
namespace AllYourChatsAreBelongToUs.Contracts.SlackIntegration {
    [DataContract]
    public class UserRequestParameters {
        [DataMember] public string  SlackToken { get; set; }
        [DataMember] public string userId { get; set; } 
    }
}