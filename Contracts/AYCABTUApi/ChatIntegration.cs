using System;
using System.Runtime.Serialization;

namespace AllYourChatsAreBelongToUs.Contracts.AYCABTUApi {
    [DataContract]
    public class ChatIntegration {
        [DataMember]
        public Guid InstanceId { get; set; }
        [DataMember]
        public Guid IntegrationId { get; set; }
        [DataMember]
        public string Name { get; set; }
    }
}