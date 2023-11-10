
using SocialNetwork.Core.Application.Interfaces.Services;
using SocialNetwork.Core.Application.ViewModels.User;
using SocialNetwork.Core.Domain.Entities;
using SocialNetwork.Core.Application.Interfaces.Repository;
using AutoMapper;

namespace SocialNetwork.Core.Application.Services
{
    public class UserService : GenericService<UserViewModel, UserSaveViewModel, UserUpdateViewModel, User>, IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
    }
}
