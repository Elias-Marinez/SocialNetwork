
namespace SocialNetwork.Core.Domain.Common
{
    public class BaseEntity
    {
        public required string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public BaseEntity()
        {
            CreatedAt = DateTime.Now;
        }
    }
}
