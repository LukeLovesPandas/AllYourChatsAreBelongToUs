using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AllYourChatsAreBelongToUs.Database.User;
using AllYourChatsAreBelongToUs.Services;
using AllYourChatsAreBelongToUs.Services.Integrations;
using AllYourChatsAreBelongToUs.Contracts.SlackIntegration;
using AllYourChatsAreBelongToUs.Contracts;
using AllYourChatsAreBelongToUs.Contracts.ViewModels;
using AllYourChatsAreBelongToUs.Contracts.Extensions;
using AllYourChatsAreBelongToUs.Contracts.ViewModels.Slack;
namespace AllYourChatsAreBelongToUs.Controllers
{
    [Route("api/users")]
    public class UsersController : Controller
    {
        private readonly Dictionary<Guid, IUserClient> _clientDictionary;
        private readonly UserService _userService;
        public UsersController(SlackIntegrationClient slackClient, UserService userService) {
            _userService = userService;

            _clientDictionary = new Dictionary<Guid, IUserClient> {
                { Constants.SlackIntegrationId, slackClient }
            };

            // Just for testing. Primes the DB with one entry
            if (_userService.GetAllUsers().Count == 0) {
                _userService.AddUser(new UserEntity {
                    FirstName =  "Luke",
                    LastName = "Dieter",
                    IsActive = true,
                    Title = "Mr.",
                    TimeZoneId = 22
                });
            }

        }
        // GET api/users
        [HttpGet]
        [Route("GetWithChatDetails")]
        public ActionResult<ChatInfoUser> GetWithChatDetails([FromQuery] Guid userId)
        {
            if (userId.Equals(Guid.Empty)) return BadRequest();
            var userEntity = _userService.GetUser(userId);
            if (userEntity == null) return StatusCode(404);
            ChatInfoUser returnUser = new ChatInfoUser();
            returnUser.MapUserEntityToBaseUser(userEntity);
            returnUser.ChatUserDetails = new List<ChatUser>();
            userEntity.ChatIntegrations?.ForEach( x => {
                _clientDictionary.TryGetValue(x.IntegrationId, out var client);
                if (client != null) {
                    BaseUserResponse response = client.GetUserInfo(x).Result;
                    if (response != null) {
                        var transformedResponse = response.CreateDerivedUserInfoFromBaseResponse(x);
                        returnUser.ChatUserDetails.Add(transformedResponse);
                    };
                }
            });
            return returnUser;
        }

        [HttpGet]
        [Route("GetWithIntegrationDetails")]
        public ActionResult<IntegrationInfoUser> GetWithIntegrationDetails([FromQuery] Guid userId)
        {
            if (userId.Equals(Guid.Empty)) return BadRequest();
            var userEntity = _userService.GetUser(userId);
            if (userEntity == null) return StatusCode(404);
            IntegrationInfoUser returnUser = userEntity.CreateIntegrationInfoUserFromUserEntity();
            return returnUser;
        }

        [HttpPost]
        public ActionResult<IntegrationInfoUser> AddUser([FromBody] IntegrationInfoUser baseUser) {
            if (baseUser == null) return BadRequest();
            var entryEntity = baseUser.CreateUserEntityFromIntegrationInfoUser();
            var addedUser = _userService.AddUser(entryEntity);
            if (addedUser == null) return StatusCode(500);
            var returnUser = addedUser.CreateIntegrationInfoUserFromUserEntity();
            return returnUser;
        }

        [HttpPut]
        public ActionResult<IntegrationInfoUser> UpdateUser([FromBody] IntegrationInfoUser baseUser) {
            if (baseUser == null) return BadRequest();
            var entryEntity = baseUser.CreateUserEntityFromIntegrationInfoUser();
            var updatedUser = _userService.UpdateUser(entryEntity);
            if (updatedUser == null) return StatusCode(500);
            var returnUser = updatedUser.CreateIntegrationInfoUserFromUserEntity();
            return returnUser;
        }

        [HttpDelete]
        public ActionResult<bool> DeleteUser([FromQuery] Guid userId) {
            if (userId.Equals(Guid.Empty)) return BadRequest();
            return _userService.DeleteUser(userId);
        }

        [HttpPost]
        [Route("integration")]
        public ActionResult<ChatIntegration> AddIntegrationForUser([FromBody] ChatIntegration chatIntegration, [FromQuery] Guid userId) {
            if (chatIntegration == null || userId.Equals(Guid.Empty)) return BadRequest();
            ChatIntegrationEntity entity = _userService.AddIntegrationToUser(chatIntegration.MapDerivedIntegrationEntity(), userId);
            if (entity == null) return StatusCode(500);
            return entity.MapDerivedChatIntegration();
        }

        [HttpPut]
        [Route("integration")]
        public ActionResult<ChatIntegration> UpdateIntegrationForUser([FromBody] ChatIntegration chatIntegration, [FromQuery] Guid userId) {
            if (chatIntegration == null || chatIntegration.InstanceId == null || userId.Equals(Guid.Empty)) return BadRequest();
            ChatIntegrationEntity entity = _userService.UpdateIntegrationForUser(chatIntegration.MapDerivedIntegrationEntity(), userId);
            if (entity == null) return StatusCode(500);
            return entity.MapDerivedChatIntegration();
        }

        [HttpDelete]
        [Route("integration")]
        public ActionResult<bool> DeleteIntegrationForUser([FromQuery] Guid instanceId, [FromQuery] Guid userId) {
            if (instanceId.Equals(Guid.Empty) || userId.Equals(Guid.Empty)) return BadRequest();            
            return _userService.DeleteIntegrationForUser(instanceId, userId);
        }

        [HttpGet]
        [Route("GetAll")]
        public ActionResult<List<UserEntity>> GetAll() {
            return _userService.GetAllUsers();
        }
    }
}
