
using Microsoft.AspNetCore.Identity;

namespace SocialNetwork.Infrastructure.Identity.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? ImageUrl { get; set; }

    }
}
