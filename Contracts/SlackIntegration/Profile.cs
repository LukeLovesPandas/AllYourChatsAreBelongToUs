using System.Runtime.Serialization;

namespace AllYourChatsAreBelongToUs.Contracts.SlackIntegration {
    [DataContract]
    public class Profile {

        [DataMember(Name="avatar_hash")]
        public string AvatarHash { get; set; }

        [DataMember(Name="status_text")]
        public string StatusText { get; set; }

        [DataMember(Name="status_emoji")]
        public string StatusEmoji { get; set; }

        [DataMember(Name="real_name")]
        public string RealName { get; set; }

        [DataMember(Name="display_name")]
        public string DisplayName { get; set; }

        [DataMember(Name="real_name_normalized")]
        public string RealNameNormalized { get; set; }

        [DataMember(Name="display_name_normalized")]
        public string DisplayNameNormalized {get; set;}

        [DataMember(Name="email")]
        public string Email { get; set; }

        [DataMember(Name="image_original")]
        public string ImageOriginal { get;set; }

        [DataMember(Name="image_24")]
        public string Image24 { get; set; }
        [DataMember(Name="image_32")]
        public string Image32 { get; set; }
        [DataMember(Name="image_48")]
        public string Image48 { get; set; }
        [DataMember(Name="image_72")]
        public string Image72 { get; set; }
        [DataMember(Name="image_192")]
        public string Image192 { get; set; }
        [DataMember(Name="image_512")]
        public string Image512 { get; set; }
        [DataMember(Name="team")]
        public string Team { get; set; }
    }

}
