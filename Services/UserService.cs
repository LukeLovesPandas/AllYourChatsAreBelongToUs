using System;
using Microsoft.EntityFrameworkCore;
using AllYourChatsAreBelongToUs.Database.User;
using AllYourChatsAreBelongToUs.Services.Integrations;
using System.Collections.Generic;
using System.Linq;
namespace AllYourChatsAreBelongToUs.Services {
    public class UserService {
        private UserContext _userContext;
        private SlackIntegrationClient _slackClient;
        public UserService(UserContext userContext, 
        SlackIntegrationClient slackClient) {
            _userContext = userContext;
            _slackClient = slackClient;
        }

        public List<UserEntity> GetAllUsers() {
            return _userContext.Users.Include("ChatIntegrations").ToList();
        }

        public UserEntity GetUser(Guid userId) {
            return _userContext.Users.Find(userId);
        }

        public UserEntity AddUser(UserEntity userToAdd) {
            UserEntity returnUser = _userContext.Users.Find(userToAdd.UserId);
            if (returnUser != null) return returnUser;
            userToAdd.UserId = Guid.NewGuid();
            returnUser = _userContext.Users.Add(userToAdd).Entity;
            _userContext.SaveChanges();
            return returnUser;
        }

        public UserEntity UpdateUser(UserEntity userToUpdate) {
            UserEntity returnUser = _userContext.Users.Find(userToUpdate.UserId);
            if (returnUser == null) return null;
            returnUser.Title = userToUpdate.Title;
            returnUser.FirstName = userToUpdate.FirstName;
            returnUser.LastName = userToUpdate.LastName;
            returnUser.IsActive = userToUpdate.IsActive;
            returnUser.TimeZoneId = userToUpdate.TimeZoneId;
            returnUser.ChatIntegrations = userToUpdate.ChatIntegrations;
            _userContext.SaveChanges();
            return returnUser;
        }

        public bool DeleteUser(Guid userId) {
            UserEntity foundUser = _userContext.Users.Find(userId);
            if (foundUser == null) return false;
            _userContext.Users.Remove(foundUser);
            _userContext.SaveChanges();
            return true;
        }

        public ChatIntegrationEntity AddIntegrationToUser (ChatIntegrationEntity integration, Guid userId) {
            UserEntity foundUser = _userContext.Users.Find(userId);
            if (foundUser == null) return null;
            if (foundUser != null && foundUser.ChatIntegrations.Exists(x => Guid.Equals(x.Id, integration.Id))) return integration;
            integration.Id = Guid.NewGuid();
            foundUser.ChatIntegrations.Add(integration);
            _userContext.SaveChanges();
            return integration;
        }

        public ChatIntegrationEntity UpdateIntegrationForUser<TChatIntegrationEntity>(ChatIntegrationEntity integration, Guid userId) where TChatIntegrationEntity : ChatIntegrationEntity {
            UserEntity foundUser = _userContext.Users.Find(userId);
            if (foundUser == null) return null;
            var existingIntegration = foundUser.ChatIntegrations.Find(x => Guid.Equals(x.Id, integration.Id));
            if (foundUser != null && existingIntegration == null) return null;
            foundUser.ChatIntegrations.Remove(existingIntegration);
            foundUser.ChatIntegrations.Add(integration);
            _userContext.SaveChanges();
            return integration;
        }

        public bool DeleteIntegrationForUser (Guid integrationInstanceId, Guid userId) {
            UserEntity foundUser = _userContext.Users.Find(userId);
            if (foundUser == null) return false;
            var existingIntegration = foundUser.ChatIntegrations.Find(x => Guid.Equals(x.Id, integrationInstanceId));
            if (foundUser != null && existingIntegration == null) return false;
            foundUser.ChatIntegrations.Remove(existingIntegration);
            _userContext.SaveChanges();
            return true;
        }
    }
}