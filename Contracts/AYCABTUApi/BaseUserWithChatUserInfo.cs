using System.Runtime.Serialization;
using System.Collections.Generic;

namespace AllYourChatsAreBelongToUs.Contracts.AYCABTUApi {
    [DataContract]
    public class BaseUserWithChatUserInfo : BaseUser {

        [DataMember]
        public List<ChatUser> ChatUserDetails {get;set;}
    }
}