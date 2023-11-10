
namespace SocialNetwork.Core.Application.Dtos.Account.Request
{
    public class AuthenticationRequest
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
