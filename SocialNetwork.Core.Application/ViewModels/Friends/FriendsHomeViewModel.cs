
using SocialNetwork.Core.Application.Dtos.Account.Response;
using SocialNetwork.Core.Application.ViewModels.Comment;
using SocialNetwork.Core.Application.ViewModels.Post;

namespace SocialNetwork.Core.Application.ViewModels.Friends
{
    public class FriendsHomeViewModel
    {
        public AuthenticationResponse? User { get; set; }
        public IEnumerable<FriendViewModel>? Friends { get; set; }
        public IEnumerable<PostViewModel>? Posts { get; set; }
        public CommentSaveViewModel? NewComment { get; set; }
        public FriendsSaveViewModel? NewFriend { get; set; }

    }
}
