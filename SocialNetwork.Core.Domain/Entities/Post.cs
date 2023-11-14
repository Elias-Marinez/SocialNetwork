
using SocialNetwork.Core.Domain.Common;

namespace SocialNetwork.Core.Domain.Entities
{
    public class Post : BaseEntity
    {
        public int PostId { get; set; }
        public required string UserId { get; set; }
        public string? ImageUrl { get; set; }

        //Navigation Property
        public ICollection<Comment>? Comments { get; set;}
    }
}
