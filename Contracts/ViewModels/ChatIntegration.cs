using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using AllYourChatsAreBelongToUs.Converters;

namespace AllYourChatsAreBelongToUs.Contracts.ViewModels {
    [DataContract]
    [JsonConverter(typeof(ChatIntegrationConverter))]
    public class ChatIntegration {
        [DataMember]
        public Guid InstanceId { get; set; }
        [DataMember]
        public Guid IntegrationId { get; set; }
        [DataMember]
        public string Name { get; set; }
    }
}