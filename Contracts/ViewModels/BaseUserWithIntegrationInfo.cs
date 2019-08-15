using System.Runtime.Serialization;
using System.Collections.Generic;

namespace AllYourChatsAreBelongToUs.Contracts.ViewModels {
    [DataContract]
    public class IntegrationInfoUser : BaseUser {

        [DataMember]
        public List<ChatIntegration> IntegrationsDetails {get;set;}
    }
}