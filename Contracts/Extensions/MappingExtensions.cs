using System;
using System.Linq;
using System.Collections.Generic;
using AllYourChatsAreBelongToUs.Contracts.ViewModels;
using AllYourChatsAreBelongToUs.Contracts.ViewModels.Slack;
using AllYourChatsAreBelongToUs.Contracts.SlackIntegration;
using AllYourChatsAreBelongToUs.Database.User;


namespace AllYourChatsAreBelongToUs.Contracts.Extensions {
    public static class MappingExtensions {

        public static bool IsSlackIntegration(this Guid integrationId) => Guid.Equals(Constants.SlackIntegrationId, integrationId);

        public static void MapBaseUserToUserEntity(this UserEntity userEntity, BaseUser baseUser) {
            userEntity.UserId = baseUser.UserId;
            userEntity.FirstName = baseUser.FirstName;
            userEntity.LastName = baseUser.LastName;            
            userEntity.Title = baseUser.Title;
            userEntity.IsActive = baseUser.IsActive;
            userEntity.TimeZoneId = baseUser.TimeZoneId;
        }  

        public static void MapIntegrationInfoUserToUserEntity(this UserEntity userEntity, IntegrationInfoUser baseUser) {            
            userEntity.MapBaseUserToUserEntity(baseUser);
            userEntity.ChatIntegrations = baseUser.IntegrationsDetails?.Select(x => x.MapDerivedIntegrationEntity()).ToList();
        }

        public static ChatIntegrationEntity MapDerivedIntegrationEntity(this ChatIntegration integration) {
            if (integration.IntegrationId.IsSlackIntegration())  {
                var slackIntegration = (integration as AllYourChatsAreBelongToUs.Contracts.ViewModels.Slack.SlackIntegration);
                return new SlackIntegrationEntity {
                    SlackToken = slackIntegration.SlackToken,
                    SlackUserId = slackIntegration.SlackUserId,
                    Id = slackIntegration.InstanceId
                };
            }
            return new ChatIntegrationEntity(integration.IntegrationId, integration.Name) { Id = integration.InstanceId };
        }

        public static ChatIntegration MapDerivedChatIntegration (this ChatIntegrationEntity integrationEntity) {
            if (integrationEntity.IntegrationId.IsSlackIntegration())  {
                var slackIntegrationEntity = (integrationEntity as SlackIntegrationEntity);
                return new AllYourChatsAreBelongToUs.Contracts.ViewModels.Slack.SlackIntegration {
                    SlackToken = slackIntegrationEntity.SlackToken,
                    SlackUserId = slackIntegrationEntity.SlackUserId,
                    InstanceId = slackIntegrationEntity.Id,
                    IntegrationId = slackIntegrationEntity.IntegrationId,
                    Name = slackIntegrationEntity.Name
                };
            }
            return new ChatIntegration {
                 InstanceId = integrationEntity.Id,
                 IntegrationId = integrationEntity.IntegrationId,
                 Name = integrationEntity.Name 
            };
        }

        public static ChatUser CreateDerivedUserInfoFromBaseResponse(this BaseUserResponse response, ChatIntegrationEntity integrationEntity) {
            if (integrationEntity.IntegrationId.IsSlackIntegration())  {
                var slackUserResponse = (response as UserResponse);
                if (slackUserResponse.IsOk) {
                    var userInfo = slackUserResponse.UserInfo;
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
                        Profile = new Contracts.ViewModels.Slack.Profile {
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
            return new ChatUser();
        }

        public static UserEntity CreateUserEntityFromIntegrationInfoUser(this IntegrationInfoUser baseUser) {
            UserEntity returnUserEntity = new UserEntity();
            returnUserEntity.MapIntegrationInfoUserToUserEntity(baseUser);
            return returnUserEntity;
        }

        public static void MapUserEntityToBaseUser(this BaseUser baseUser, UserEntity userEntity) {
            baseUser.UserId = userEntity.UserId;
            baseUser.FirstName = userEntity.FirstName;
            baseUser.LastName = userEntity.LastName;            
            baseUser.Title = userEntity.Title;
            baseUser.IsActive = userEntity.IsActive;
            baseUser.TimeZoneId = userEntity.TimeZoneId;
        }

        public static UserEntity CreateUserEntityFromBaseUser(this BaseUser baseUser) {
            var userEntity = new UserEntity();
            userEntity.MapBaseUserToUserEntity(baseUser);
            return userEntity;
        }

        public static BaseUser CreateBaseUserFromUserEntity(this UserEntity userEntity) {
            var baseUser = new BaseUser();
            baseUser.MapUserEntityToBaseUser(userEntity);
            return baseUser;
        }

        public static void MapUserEntityToIntegrationInfoUser(this IntegrationInfoUser integrationInfoUser, UserEntity userEntity) {
            integrationInfoUser.MapUserEntityToBaseUser(userEntity);
            List<ChatIntegration> integrations = userEntity.ChatIntegrations?.Select(x => x.MapDerivedChatIntegration()).ToList();
            integrationInfoUser.IntegrationsDetails = integrations;
        }

        public static IntegrationInfoUser CreateIntegrationInfoUserFromUserEntity(this UserEntity userEntity) {
            var returnIntegrationInfoUser = new IntegrationInfoUser();
            returnIntegrationInfoUser.MapUserEntityToIntegrationInfoUser(userEntity);
            return returnIntegrationInfoUser;
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
                Profile = new Contracts.ViewModels.Slack.Profile {
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