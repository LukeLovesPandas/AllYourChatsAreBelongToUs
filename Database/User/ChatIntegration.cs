using System;
using System.ComponentModel.DataAnnotations;

namespace AllYourChatsAreBelongToUs.Database.User {
    public class ChatIntegration {
        [Key]
        public Guid InstanceId { get; set; }
        public Guid IntegrationId { get; set; }
        public string Name { get; set; }
        
        public ChatIntegration(Guid integrationId, string name) {
            IntegrationId = integrationId;
            Name = name;
        }
    }
}