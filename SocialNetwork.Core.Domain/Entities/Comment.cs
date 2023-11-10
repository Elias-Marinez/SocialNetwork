
namespace SocialNetwork.Core.Domain.Entities
{
    public class Comment
    {
        public int CommentId { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }
        public required string Content {  get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation Property
        public Post? Post { get; set; }  
        public User? User { get; set; }

    }
}
