using System;

using AllYourChatsAreBelongToUs.Contracts;
using AllYourChatsAreBelongToUs.Contracts.ViewModels;
using AllYourChatsAreBelongToUs.Contracts.ViewModels.Slack;
using Newtonsoft.Json.Linq;
namespace AllYourChatsAreBelongToUs.Converters {
    public class ChatIntegrationConverter : JsonCreationConverter<ChatIntegration> {
        protected override ChatIntegration Create(Type objectType, JObject jObject) {
            if (jObject == null) throw new ArgumentNullException("jObject");
            if (jObject["integrationId"] != null) {
                if (string.Equals(Constants.SlackIntegrationId.ToString(), jObject["integrationId"].ToString())) {
                    return new SlackIntegration();
                }
            }
            return new ChatIntegration();
        }
    }
}

