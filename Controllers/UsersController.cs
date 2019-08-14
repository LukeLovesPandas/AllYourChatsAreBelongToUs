using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AllYourChatsAreBelongToUs.Database.User;
using AllYourChatsAreBelongToUs.Services;
using AllYourChatsAreBelongToUs.Services.Integrations;
using AllYourChatsAreBelongToUs.Contracts.SlackIntegration;
using AllYourChatsAreBelongToUs.Contracts;
using AllYourChatsAreBelongToUs.Contracts.AYCABTUApi;
using AllYourChatsAreBelongToUs.Contracts.Extensions;
using AllYourChatsAreBelongToUs.Contracts.AYCABTUApi.Slack;
namespace AllYourChatsAreBelongToUs.Controllers
{
    [Route("api/users")]
    public class UsersController : Controller
    {
        private readonly SlackIntegrationClient _slackClient;

        // private readonly Dictionary<Guid, IUserClient<T, T>> _userClientDictionary;
        private readonly UserService _userService;
        public UsersController(SlackIntegrationClient slackClient, UserService userService) {
            _slackClient = slackClient;
            _userService = userService;

            if (_userService.GetAllUsers().Count == 0) {
                _userService.AddUser(new UserEntity {
                    FirstName =  "Luke",
                    LastName = "Dieter",
                    IsActive = true,
                    Title = "Mr.",
                    TimeZoneId = 22,
                    ChatIntegrations = new List<ChatIntegrationEntity> {
                        new SlackIntegrationEntity {
                            ApplicationId = "1",
                            SlackUserId = "1", 
                        }
                    }
                });
            }

        }
        // GET api/users
        [HttpGet]
        public async Task<ActionResult<BaseUserWithChatUserInfo>> Get([FromQuery] Guid userId)
        {
            BaseUserWithChatUserInfo returnUser = new BaseUserWithChatUserInfo();
            var userEntity = _userService.GetUser(userId);
            if (userEntity == null) return StatusCode(404);
            returnUser.MapUserEntityToBaseUser(userEntity);
            SlackIntegrationEntity sIE = (SlackIntegrationEntity) userEntity.ChatIntegrations.Find(x => Guid.Equals(x.IntegrationId, Constants.SlackIntegrationId));

            UserInfo userInfo = await _slackClient.GetUserInfo(new UserRequestParameters {
                applicationId = sIE.ApplicationId,
                userId = sIE.SlackUserId
            });

            if (userInfo == null) return StatusCode(404);
            returnUser.ChatUserDetails = new List<ChatUser>{
                userInfo.CreateNewSlackUserFromUserInfo()
            };
            return returnUser;
        }

        [HttpGet]
        [Route("GetAll")]
        public ActionResult<List<UserEntity>> GetAll() {
            return _userService.GetAllUsers();
        }

        [HttpGet]
        [Route("AddOne")]
        public ActionResult<List<UserEntity>> PostOne() {
                // Temporary for testing
            var addedUser = _userService.AddUser(new UserEntity {
                UserId = Guid.NewGuid(),
                FirstName =  "Luke",
                LastName = "Dieter",
                IsActive = true,
                Title = "Mr.",
                TimeZoneId = 22,
                ChatIntegrations = new List<ChatIntegrationEntity> {
                    new SlackIntegrationEntity {
                        ApplicationId = "1",
                        SlackUserId = "1", 
                    }
                }
            });
            return Ok(addedUser);
        }
    }
}
