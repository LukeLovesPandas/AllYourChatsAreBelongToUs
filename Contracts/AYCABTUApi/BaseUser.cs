using System;
using System.Runtime.Serialization;

namespace AllYourChatsAreBelongToUs.Contracts.AYCABTUApi {
    [DataContract]
    public class BaseUser {

        [DataMember]
        public Guid UserId { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public bool IsActive { get; set; }
        [DataMember]
        public int TimeZoneId { get; set; }
    }
}