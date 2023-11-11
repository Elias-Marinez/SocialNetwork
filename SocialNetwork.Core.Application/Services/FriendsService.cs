
using AutoMapper;
using SocialNetwork.Core.Application.Interfaces.Repository;
using SocialNetwork.Core.Application.Interfaces.Services;
using SocialNetwork.Core.Application.ViewModels.Friends;
using SocialNetwork.Core.Domain.Entities;

namespace SocialNetwork.Core.Application.Services
{
    public class FriendsService : GenericService<FriendsViewModel, FriendsSaveViewModel, FriendsViewModel, Friends>,  IFriendsService
    {
        private readonly IFriendsRepository _repository;
        private readonly IMapper _mapper;

        public FriendsService(IFriendsRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
    }
}
