
using AutoMapper;
using SocialNetwork.Core.Application.Interfaces.Repository;
using SocialNetwork.Core.Application.Interfaces.Services;
using SocialNetwork.Core.Application.ViewModels.Post;
using SocialNetwork.Core.Domain.Entities;

namespace SocialNetwork.Core.Application.Services
{
    public class PostService : GenericService<PostViewModel, PostSaveViewModel, PostUpdateViewModel, Post>, IPostService
    {
        private readonly IPostRepository _repository;
        private readonly IMapper _mapper;

        public PostService(IPostRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

    }
}
