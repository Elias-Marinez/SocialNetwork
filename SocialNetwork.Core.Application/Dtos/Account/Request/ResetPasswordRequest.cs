
namespace SocialNetwork.Core.Application.Dtos.Account.Request
{
    public class ResetPasswordRequest
    {
        public required string Email { get; set; }
        public required string Token { get; set; }
        public required string Password { get; set; }
        public required string ConfirmPassword { get; set; }
    }
}
