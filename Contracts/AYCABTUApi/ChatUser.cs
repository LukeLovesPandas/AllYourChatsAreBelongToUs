using System;
using System.Runtime.Serialization;

namespace AllYourChatsAreBelongToUs.Contracts.AYCABTUApi {
    [DataContract]
    public class ChatUser {

        [DataMember]
        public Guid Id { get; set; }
        [DataMember] 
        public string UserName { get; set; }
    }
}