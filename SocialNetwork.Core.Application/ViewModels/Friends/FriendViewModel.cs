
using SocialNetwork.Core.Application.ViewModels.Post;

namespace SocialNetwork.Core.Application.ViewModels.Friends
{
    public class FriendViewModel
    {
        public int FriendsId { get; set; }
        public string Id { get; set; }
        public required string Name { get; set; }
        public required string LastName { get; set; }
        public required string Username { get; set; }
    }
}
