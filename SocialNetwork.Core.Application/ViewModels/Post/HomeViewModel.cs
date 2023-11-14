
using SocialNetwork.Core.Application.Dtos.Account.Response;
using SocialNetwork.Core.Application.ViewModels.Comment;

namespace SocialNetwork.Core.Application.ViewModels.Post
{
    public class HomeViewModel
    {
        public AuthenticationResponse? User { get; set; }
        public PostSaveViewModel? NewPost { get; set; }
        public CommentSaveViewModel? NewComment { get; set; }
        public List<PostViewModel>? Posts { get; set; }
    }
}
