using System;
using System.Web;
using System.Security;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using AllYourChatsAreBelongToUs.Contracts.SlackIntegration;

namespace AllYourChatsAreBelongToUs.Services.Integrations {

    public class SlackIntegrationClient : IUserClient<UserRequestParameters, UserInfo> {

        public HttpClient Client { get; set; }
        public SlackIntegrationClient(HttpClient client) {
            client.BaseAddress = new Uri("https://slack.com");
            Client = client;
        }

        public async Task<UserInfo> GetUserInfo(UserRequestParameters userRequestParameters) {
            UserInfo userInfo = null; 
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["token"] = userRequestParameters.applicationId;
            query["user"] = userRequestParameters.userId;
            var response = await Client.GetAsync($"/api/users.info?{query.ToString()}");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            userInfo = JsonConvert.DeserializeObject<UserResponse>(responseString).UserInfo;
 
            return userInfo;
        }
    }
}