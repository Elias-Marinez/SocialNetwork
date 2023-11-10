
using SocialNetwork.Core.Application.ViewModels.Comment;
using SocialNetwork.Core.Application.ViewModels.Friends;
using SocialNetwork.Core.Application.ViewModels.Post;

namespace SocialNetwork.Core.Application.ViewModels.User
{
    public class UserViewModel
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

        public IEnumerable<PostViewModel>? Posts { get; set; }
        public IEnumerable<CommentViewModel>? Comments { get; set; }
        public IEnumerable<FriendViewModel>? Friends { get; set; }

    }
}
