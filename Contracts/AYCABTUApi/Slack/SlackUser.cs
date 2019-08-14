using System.Runtime.Serialization;

namespace AllYourChatsAreBelongToUs.Contracts.AYCABTUApi.Slack {

    [DataContract]
    public class SlackUser : ChatUser {
        [DataMember] public string TeamId { get; set;}

        [DataMember] public bool Deleted { get; set; }
        
        [DataMember] public string Color { get; set; }

        [DataMember] public string RealName { get; set; }

        [DataMember] public string  TimeZone { get; set; }

        [DataMember] public string TimeZoneLabel { get; set; }

        [DataMember] public int TimeZoneOffset { get; set; }

        [DataMember] public Profile Profile { get; set; }

        [DataMember] public bool IsAdmin { get; set; }
        [DataMember] public bool IsOwner { get; set; }
        
        [DataMember] public bool IsPrimaryOwner { get; set; }

        [DataMember] public bool IsRestricted { get; set; }

        [DataMember] public bool IsUltraRestricted { get; set; }

        [DataMember] public bool IsBot { get; set; }

        [DataMember] public long Updated { get; set; }

        [DataMember] public bool IsAppUser { get; set; }

        [DataMember] public bool Has2FA { get; set;}
    }

}
