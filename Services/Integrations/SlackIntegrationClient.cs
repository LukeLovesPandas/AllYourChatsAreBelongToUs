using System;
using System.Web;
using System.Security;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using AllYourChatsAreBelongToUs.Contracts.SlackIntegration;
using AllYourChatsAreBelongToUs.Database.User;
using AllYourChatsAreBelongToUs.Contracts;

namespace AllYourChatsAreBelongToUs.Services.Integrations {

    public class SlackIntegrationClient : IUserClient {

        public HttpClient Client { get; set; }
        public SlackIntegrationClient(HttpClient client) {
            client.BaseAddress = new Uri("https://slack.com");
            Client = client;
        }

        public async Task<BaseUserResponse> GetUserInfo(ChatIntegrationEntity chatIntegrationEntity) {
            return await GetUserInfo(chatIntegrationEntity as SlackIntegrationEntity);
        } 

        public async Task<UserResponse> GetUserInfo(SlackIntegrationEntity slackIntegrationEntity) {
            UserResponse userResponse = null; 
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["token"] = slackIntegrationEntity.SlackToken;
            query["user"] = slackIntegrationEntity.SlackUserId;
            var response = await Client.GetAsync($"/api/users.info?{query.ToString()}");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            userResponse = JsonConvert.DeserializeObject<UserResponse>(responseString);
 
            return userResponse;
        }
    }
}