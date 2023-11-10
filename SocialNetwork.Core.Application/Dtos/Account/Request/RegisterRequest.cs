
namespace SocialNetwork.Core.Application.Dtos.Account.Request
{
    public class RegisterRequest
    {
        public required string Name { get; set; }
        public required string LastName { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Email { get; set; }
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public required string ConfirmPassword { get; set; }
        public required string ImageUrl { get; set; }
    }
}
