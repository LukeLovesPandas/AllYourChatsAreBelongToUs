using System.Runtime.Serialization;
using System.Collections.Generic;

namespace AllYourChatsAreBelongToUs.Contracts.AYCABTUApi {
    [DataContract]
    public class BaseUserWithIntegrationInfo : BaseUser {

        [DataMember]
        public List<ChatIntegration> IntegrationsDetails {get;set;}
    }
}