
namespace SocialNetwork.Core.Domain.Entities
{
    public class Post
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string? ImageUrl { get; set; }
        public required string Content { get; set; }
        public DateTime CreatedAt { get; set;}

        //Navigation Property
        public User? User { get; set;}
        public ICollection<Comment>? Comments { get; set;}
    }
}
