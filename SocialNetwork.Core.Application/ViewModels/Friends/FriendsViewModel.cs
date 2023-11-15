﻿
using SocialNetwork.Core.Application.Dtos.Account.Response;
using SocialNetwork.Core.Application.ViewModels.Post;

namespace SocialNetwork.Core.Application.ViewModels.Friends
{
    public class FriendsViewModel
    {
        public int FriendsId { get; set; }
        public string UserId { get; set; }
        public string FriendId { get; set; }
    }
}
