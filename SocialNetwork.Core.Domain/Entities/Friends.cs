
namespace SocialNetwork.Core.Domain.Entities
{
    public class Friends
    {
        public int FriendsId { get; set; }
        public required string UserId { get; set; }
        public required string FriendId { get; set; }
    }
}
