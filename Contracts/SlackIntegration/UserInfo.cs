using System.Runtime.Serialization;

namespace AllYourChatsAreBelongToUs.Contracts.SlackIntegration {
    [DataContract]
    public class UserInfo {

        [DataMember(Name="id")] public string Id { get; set; }
        [DataMember(Name="team_id")] public string TeamId { get; set;}
        [DataMember(Name="name")] public string Name { get; set; }
        [DataMember(Name="deleted")] public bool Deleted { get; set; }
        
        [DataMember(Name="color")] public string Color { get; set; }

        [DataMember(Name="real_name")] public string RealName { get; set; }

        [DataMember(Name="tz")] public string  TimeZone { get; set; }

        [DataMember(Name="tz_label")] public string TimeZoneLabel { get; set; }

        [DataMember(Name="tz_offset")] public int TimeZoneOffset { get; set; }

        [DataMember(Name="profile")] public Profile Profile { get; set; }

        [DataMember(Name="is_admin")] public bool IsAdmin { get; set; }
        [DataMember(Name="is_owner")] public bool IsOwner { get; set; }
        
        [DataMember(Name="is_primary_owner")] public bool IsPrimaryOwner { get; set; }

        [DataMember(Name="is_restricted")] public bool IsRestricted { get; set; }

        [DataMember(Name="is_ultra_restricted")] public bool IsUltraRestricted { get; set; }

        [DataMember(Name="is_bot")] public bool IsBot { get; set; }

        [DataMember(Name="updated")] public long Updated { get; set; }

        [DataMember(Name="is_app_user")] public bool IsAppUser { get; set; }

        [DataMember(Name="has_2fa")] public bool Has2FA { get; set;}
    }

}    

