
using SocialNetwork.Core.Application.Dtos.Account.Response;
using SocialNetwork.Core.Application.ViewModels.Comment;

namespace SocialNetwork.Core.Application.ViewModels.Post
{
    public class PostViewModel
    {
        public int PostId { get; set; }
        public string UserId { get; set; }
        public string? ImageUrl { get; set; }
        public required string Content { get; set; }
        public DateTime CreatedAt { get; set; }

        //Navigation Property
        public List<CommentViewModel>? Comments { get; set; }
        public AuthenticationResponse? User { get; set; }

    }
}
