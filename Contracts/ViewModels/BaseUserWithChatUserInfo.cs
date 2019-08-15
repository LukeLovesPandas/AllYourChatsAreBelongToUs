using System.Runtime.Serialization;
using System.Collections.Generic;

namespace AllYourChatsAreBelongToUs.Contracts.ViewModels {
    [DataContract]
    public class ChatInfoUser : BaseUser {

        [DataMember]
        public List<ChatUser> ChatUserDetails {get;set;}
    }
}