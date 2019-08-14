using AllYourChatsAreBelongToUs.Contracts.AYCABTUApi;
using AllYourChatsAreBelongToUs.Contracts.SlackIntegration;
using AllYourChatsAreBelongToUs.Database.User;
using AllYourChatsAreBelongToUs.Contracts.AYCABTUApi.Slack;
namespace AllYourChatsAreBelongToUs.Contracts.Extensions {
    public static class MappingExtensions {
        public static void MapUserEntityToBaseUser(this BaseUser baseUser, UserEntity userEntity) {
            baseUser.UserId = userEntity.UserId;
            baseUser.FirstName = userEntity.FirstName;
            baseUser.LastName = userEntity.LastName;            
            baseUser.Title = userEntity.Title;
            baseUser.IsActive = userEntity.IsActive;
            baseUser.TimeZoneId = userEntity.TimeZoneId;
        }

        public static SlackUser CreateNewSlackUserFromUserInfo(this UserInfo userInfo) {
            return new SlackUser {             
                Id = userInfo.Id,
                TeamId = userInfo.TeamId,
                UserName = userInfo.Name,
                Deleted = userInfo.Deleted,
                Color = userInfo.Color,
                RealName = userInfo.RealName,
                TimeZone = userInfo.TimeZone,
                TimeZoneLabel = userInfo.TimeZoneLabel,
                TimeZoneOffset = userInfo.TimeZoneOffset,
                IsAdmin = userInfo.IsAdmin,
                IsOwner = userInfo.IsOwner,
                IsPrimaryOwner = userInfo.IsPrimaryOwner,
                IsRestricted = userInfo.IsRestricted,
                IsUltraRestricted = userInfo.IsUltraRestricted,
                IsBot = userInfo.IsBot,
                Updated = userInfo.Updated,
                IsAppUser = userInfo.IsAppUser,
                Has2FA = userInfo.Has2FA,
                Profile = new Contracts.AYCABTUApi.Slack.Profile {
                    AvatarHash = userInfo.Profile.AvatarHash,
                    StatusText = userInfo.Profile.StatusText,
                    StatusEmoji = userInfo.Profile.StatusEmoji,
                    RealName = userInfo.Profile.RealName,
                    DisplayName = userInfo.Profile.DisplayName,
                    RealNameNormalized = userInfo.Profile.RealNameNormalized,
                    DisplayNameNormalized = userInfo.Profile.DisplayNameNormalized,
                    Email = userInfo.Profile.Email,
                    ImageOriginal = userInfo.Profile.ImageOriginal,
                    Image24 = userInfo.Profile.Image24,
                    Image32 = userInfo.Profile.Image32,
                    Image48 = userInfo.Profile.Image48,
                    Image72 = userInfo.Profile.Image72,
                    Image192 = userInfo.Profile.Image192,
                    Image512 = userInfo.Profile.Image512,
                    Team = userInfo.Profile.Team
                }
            };  
        }
    }

}