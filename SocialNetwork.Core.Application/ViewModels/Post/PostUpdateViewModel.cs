
using Microsoft.AspNetCore.Http;
using SocialNetwork.Core.Application.Dtos.Account.Response;

namespace SocialNetwork.Core.Application.ViewModels.Post
{
    public class PostUpdateViewModel
    {
        public int PostId { get; set; }
        public string UserId { get; set; }
        public string? ImageUrl { get; set; }
        public required string Content { get; set; }
        public IFormFile? Image { get; set; }
        public AuthenticationResponse? User { get; set; }

    }
}
