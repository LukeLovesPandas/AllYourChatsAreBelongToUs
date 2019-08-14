using System;
using System.ComponentModel.DataAnnotations;

namespace AllYourChatsAreBelongToUs.Database.User {
    public class ChatIntegrationEntity{
        [Key]
        public Guid Id { get; set; }
        public Guid IntegrationId { get; set; }
        public string Name { get; set; }
        
        public ChatIntegrationEntity(Guid integrationId, string name) {
            IntegrationId = integrationId;
            Name = name;
        }
    }
}