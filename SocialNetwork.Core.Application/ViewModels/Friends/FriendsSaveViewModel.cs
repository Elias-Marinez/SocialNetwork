
namespace SocialNetwork.Core.Application.ViewModels.Friends
{
    public class FriendsSaveViewModel
    {
        public string UserId { get; set; }
        public string FriendUserName { get; set; }
        public string? FriendId { get; set; }
        public bool HasError { get; set; }
        public string? Error { get; set; }
    }
}
