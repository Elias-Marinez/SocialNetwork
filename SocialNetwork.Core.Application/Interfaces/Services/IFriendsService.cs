
using SocialNetwork.Core.Application.ViewModels.Friends;
using SocialNetwork.Core.Domain.Entities;

namespace SocialNetwork.Core.Application.Interfaces.Services
{
    public interface IFriendsService : IGenericService<FriendsViewModel, FriendsSaveViewModel, FriendsViewModel, Friends>
    {

    }
}
