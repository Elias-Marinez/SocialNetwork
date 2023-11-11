
namespace SocialNetwork.Core.Application.Dtos.Account.Request
{
    public class AuthenticationRequest
    {
        public required string UserName { get; set; }
        public required string Password { get; set; }
    }
}
