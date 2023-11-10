
using AutoMapper;
using SocialNetwork.Core.Application.Interfaces.Repository;
using SocialNetwork.Core.Application.Interfaces.Services;
using SocialNetwork.Core.Application.ViewModels.Comment;
using SocialNetwork.Core.Domain.Entities;

namespace SocialNetwork.Core.Application.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _repository;
        private readonly IMapper _mapper;

        public CommentService(ICommentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task Add(CommentSaveViewModel vm)
        {
            Comment entity = _mapper.Map<Comment>(vm);
            await _repository.AddAsync(entity);
        }
    }
}
