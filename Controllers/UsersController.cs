using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AllYourChatsAreBelongToUs.Database.User;
using AllYourChatsAreBelongToUs.Services.Integrations;
using AllYourChatsAreBelongToUs.Contracts.SlackIntegration;

namespace AllYourChatsAreBelongToUs.Controllers
{
    [Route("api/users")]
    public class UsersController : Controller
    {
        private readonly SlackIntegrationClient _slackClient;
        private readonly UserContext _userContext;
        public UsersController(SlackIntegrationClient slackClient, UserContext userContext) {
            _slackClient = slackClient;
            _userContext = userContext;

            if (_userContext.Users.Count() == 0) {
                // Temporary for testing
                _userContext.Users.Add(new UserEntity {
                    UserId = Guid.NewGuid(),
                    FirstName =  "Luke",
                    LastName = "Dieter",
                    IsActive = true,
                    Title = "Mr.",
                    TimeZoneId = 22,
                    ChatIntegrations = new List<ChatIntegration> {
                        new SlackIntegration {
                            ApplicationId = "1",
                            SlackUserId = "1", 
                        }
                    }
                });
                _userContext.Users.Add(new UserEntity {
                    UserId = Guid.NewGuid(),
                    FirstName =  "Luke3",
                    LastName = "Dieter3",
                    IsActive = true,
                    Title = "Mr.",
                    TimeZoneId = 22,
                    ChatIntegrations = new List<ChatIntegration> {
                        new SlackIntegration {
                            ApplicationId = "1",
                            SlackUserId = "1", 
                        }
                    }
                });
                _userContext.SaveChanges();
            }

        }
        // GET api/users
        [HttpGet]
        public async Task<UserInfo> Get([FromQuery] string appId, [FromQuery] string userId)
        {
            var userInfo = await _slackClient.GetUserInfo(appId,userId);
            return userInfo;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult<List<UserEntity>>> GetAll() {
            return await _userContext.Users.Include("ChatIntegrations").ToListAsync();
        }

        [HttpGet]
        [Route("AddOne")]
        public async Task<ActionResult<List<UserEntity>>> PostOne() {
                // Temporary for testing
            var theGuid = Guid.NewGuid();
            _userContext.Users.Add(new UserEntity {
                UserId = theGuid,
                FirstName =  "Luke",
                LastName = "Dieter",
                IsActive = true,
                Title = "Mr.",
                TimeZoneId = 22,
                ChatIntegrations = new List<ChatIntegration> {
                    new SlackIntegration {
                        ApplicationId = "1",
                        SlackUserId = "1", 
                    }
                }
            });
            _userContext.SaveChanges();
            var savedItem = await _userContext.Users.FindAsync(theGuid);
            return Ok(savedItem);
        }
    }
}
