using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AllYourChatsAreBelongToUs.Database.User {
    public class UserEntity {
        [Key]
        public Guid UserId { get; set; }

        public string Title { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
    
        public bool IsActive { get; set; }
    
        public int TimeZoneId { get; set; }

        public List<ChatIntegration> ChatIntegrations  { get; set; }
    }
}