
using Microsoft.AspNetCore.Http;

namespace SocialNetwork.Core.Application.ViewModels.Post
{
    public class PostSaveViewModel
    {
        public string UserId { get; set; }
        public string? ImageUrl { get; set; }
        public required string Content { get; set; }
        public IFormFile? Image { get; set; }
    }
}
