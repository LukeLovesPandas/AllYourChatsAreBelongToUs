using System.Threading.Tasks;
using AllYourChatsAreBelongToUs.Contracts;
using AllYourChatsAreBelongToUs.Database.User;

namespace AllYourChatsAreBelongToUs.Services.Integrations {
    public interface IUserClient
    {
        Task<BaseUserResponse> GetUserInfo(ChatIntegrationEntity integrationEntity);
    }
}