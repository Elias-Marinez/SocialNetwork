
using SocialNetwork.Core.Application.Dtos.Account.Response;

namespace SocialNetwork.Core.Application.ViewModels.Comment
{
    public class CommentViewModel
    {
        public int CommentId { get; set; }
        public string UserId { get; set; }
        public int PostId { get; set; }
        public required string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public AuthenticationResponse? User {  get; set; }
    }
}
