using System.Runtime.Serialization;

namespace AllYourChatsAreBelongToUs.Contracts.SlackIntegration {
    [DataContract]
    public class Profile {

        [DataMember]
        public string AvatarHash { get; set; }

        [DataMember]
        public string StatusText { get; set; }

        [DataMember]
        public string StatusEmoji { get; set; }

        [DataMember]
        public string RealName { get; set; }

        [DataMember]
        public string DisplayName { get; set; }

        [DataMember]
        public string RealNameNormalized { get; set; }

        [DataMember]
        public string DisplayNameNormalized {get; set;}

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string ImageOriginal { get;set; }

        [DataMember]
        public string Image24 { get; set; }
        [DataMember]
        public string Image32 { get; set; }
        [DataMember]
        public string Image48 { get; set; }
        [DataMember]
        public string Image72 { get; set; }
        [DataMember]
        public string Image192 { get; set; }
        [DataMember]
        public string Image512 { get; set; }
        [DataMember]
        public string Team { get; set; }
    }

}
