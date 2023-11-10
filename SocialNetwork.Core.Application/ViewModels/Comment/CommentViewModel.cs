
namespace SocialNetwork.Core.Application.ViewModels.Comment
{
    public class CommentViewModel
    {
        public int CommentId { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }
        public required string Content { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
