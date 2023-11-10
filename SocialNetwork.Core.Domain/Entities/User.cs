
namespace SocialNetwork.Core.Domain.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public required string Name { get; set; }
        public required string LastName { get; set; }   
        public required string Phone { get; set; }
        public required string Email { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
        public required string ImageUrl { get; set; }
        public bool Status { get; set; }

        // Navegation Property
        public ICollection<Post>? Posts { get; set; }
        public ICollection<Comment>? Comments { get; set; }
        public ICollection<User>? Friends { get; set; }
    }
}
