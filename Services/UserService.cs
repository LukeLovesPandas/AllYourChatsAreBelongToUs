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
            _userContext.Entry(returnUser).CurrentValues.SetValues(userToUpdate);

            //Remove old entries
            returnUser.ChatIntegrations?.ForEach(x => {
                var matched = userToUpdate.ChatIntegrations?.Find(y => y.Id.Equals(x.Id));
                if (matched == null)  _userContext.ChatIntegrations.Remove(x);             
            });

            //Add and update new
            userToUpdate.ChatIntegrations?.ForEach(x => {
                var matched = returnUser.ChatIntegrations?.Find(y => y.Id.Equals(x.Id));
                if (matched != null) {
                    _userContext.Entry(matched).CurrentValues.SetValues(x);
                } else {
                    x.Id = Guid.NewGuid();
                    returnUser.ChatIntegrations.Add(x);
                }
            });

            _userContext.SaveChanges();
            return returnUser;
        }

        public bool DeleteUser(Guid userId) {
            UserEntity foundUser = _userContext.Users.Find(userId);
            if (foundUser == null) return false;
            foundUser.ChatIntegrations?.ForEach(x => {
                _userContext.ChatIntegrations.Remove(x);             
            });
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

        public ChatIntegrationEntity UpdateIntegrationForUser(ChatIntegrationEntity integration, Guid userId) {
            UserEntity foundUser = _userContext.Users.Find(userId);
            if (foundUser == null) return null;
            var existingIntegration = foundUser.ChatIntegrations?.Find(x => Guid.Equals(x.Id, integration.Id));
            if (foundUser != null && existingIntegration == null) return null;
            _userContext.Entry(existingIntegration).CurrentValues.SetValues(integration);
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