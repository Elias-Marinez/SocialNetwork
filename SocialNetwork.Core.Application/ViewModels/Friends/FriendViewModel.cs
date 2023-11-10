
using SocialNetwork.Core.Application.ViewModels.Post;

namespace SocialNetwork.Core.Application.ViewModels.Friends
{
    public class FriendViewModel
    {
        public int UserId { get; set; }
        public required string Name { get; set; }
        public required string LastName { get; set; }
        public required string Phone { get; set; }
        public required string Email { get; set; }
        public required string Username { get; set; }
        public required string ImageUrl { get; set; }

        public IEnumerable<PostViewModel>? Posts { get; set; }
    }
}
