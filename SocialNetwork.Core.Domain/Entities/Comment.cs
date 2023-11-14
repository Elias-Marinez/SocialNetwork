
using SocialNetwork.Core.Domain.Common;

namespace SocialNetwork.Core.Domain.Entities
{
    public class Comment : BaseEntity
    {
        public int CommentId { get; set; }
        public required string UserId { get; set; }
        public int PostId { get; set; }
        
        // Navigation Property
        public Post? Post { get; set; }  

    }
}
