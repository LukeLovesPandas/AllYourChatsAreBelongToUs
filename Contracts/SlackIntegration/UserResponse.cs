using System.Runtime.Serialization;
namespace AllYourChatsAreBelongToUs.Contracts.SlackIntegration {
    [DataContract]
    public class UserResponse {
        [DataMember(Name="ok")] public bool IsOk {get;set;}
        [DataMember(Name="user")] public UserInfo UserInfo {get;set;} 
    }
}