using System.Threading.Tasks;

namespace AllYourChatsAreBelongToUs.Services.Integrations {
    public interface IUserClient<TUserRequest, TUserResponse>
    {
        Task<TUserResponse> GetUserInfo(TUserRequest userRequest);
    }
}