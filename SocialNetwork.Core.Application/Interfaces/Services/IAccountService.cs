
using SocialNetwork.Core.Application.Dtos.Account.Request;
using SocialNetwork.Core.Application.Dtos.Account.Response;

namespace SocialNetwork.Core.Application.Interfaces.Services
{
    public interface IAccountService
    {
        Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request);
        Task<string> ConfirmAccountAsync(string userId, string token);
        Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordRequest request);
        Task<AuthenticationResponse> GetUserAsync(string id);
        Task<AuthenticationResponse> GetUserByUserNameAsync(string userName);
        Task<RegisterResponse> RegisterUserAsync(RegisterRequest request, string origin);
        Task SignOutAsync();
    }
}
