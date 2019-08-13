using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Security;
using AllYourChatsAreBelongToUs.Services.Integrations;
using AllYourChatsAreBelongToUs.Contracts.SlackIntegration;

namespace AllYourChatsAreBelongToUs.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly SlackIntegrationClient _slackClient;
        public UsersController(SlackIntegrationClient slackClient) {
            _slackClient = slackClient;
        }
        // GET api/users
        [HttpGet]
        public async Task<UserInfo> Get([FromQuery] string appId, [FromQuery] string userId)
        {
            var userInfo = await _slackClient.GetUserInfo(appId,userId);
            return userInfo;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
