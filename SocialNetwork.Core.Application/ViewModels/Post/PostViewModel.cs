
using SocialNetwork.Core.Application.ViewModels.Comment;
using SocialNetwork.Core.Application.ViewModels.User;

namespace SocialNetwork.Core.Application.ViewModels.Post
{
    public class PostViewModel
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string? ImageUrl { get; set; }
        public required string Content { get; set; }
        public DateTime CreatedAt { get; set; }

        //Navigation Property
        public IEnumerable<CommentViewModel>? Comments { get; set; }
    }
}
