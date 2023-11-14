
using AutoMapper;
using Microsoft.AspNetCore.Http;
using SocialNetwork.Core.Application.Dtos.Account.Response;
using SocialNetwork.Core.Application.Helpers;
using SocialNetwork.Core.Application.Interfaces.Repository;
using SocialNetwork.Core.Application.Interfaces.Services;
using SocialNetwork.Core.Application.ViewModels.Comment;
using SocialNetwork.Core.Application.ViewModels.Post;
using SocialNetwork.Core.Domain.Entities;

namespace SocialNetwork.Core.Application.Services
{
    public class PostService : GenericService<PostViewModel, PostSaveViewModel, PostUpdateViewModel, Post>, IPostService
    {
        private readonly IPostRepository _repository;
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        private readonly AuthenticationResponse _user;

        public PostService(IPostRepository repository, IHttpContextAccessor httpContextAccessor, IMapper mapper, IAccountService accountService) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _user = httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
            _accountService = accountService;

        }
        public virtual async Task Update(PostUpdateViewModel vm, int id)
        {
            Post entity = _mapper.Map<Post>(vm);
            await _repository.UpdateAsync(entity, id);
        }
        public async Task<PostUpdateViewModel> GetUpdateViewModel(int id)
        {
            PostUpdateViewModel model = await base.GetById(id);

            model.User = _user;

            return model;
        }

        public async Task<HomeViewModel> GetHomeViewModel()
        {
            HomeViewModel home = new HomeViewModel()
            {
                User = _user
            };

            List<PostViewModel> posts = await base.GetWithAll();

            home.Posts = posts
                .Where(post => post.UserId == _user.Id)
                .OrderByDescending(post => post.CreatedAt)
                .Select(async post =>
                {
                    var comments = _mapper.Map<List<CommentViewModel>>(post.Comments);

                    foreach (var comment in comments)
                    {
                        comment.User = await _accountService.GetUserAsync(comment.UserId);
                    }

                    return new PostViewModel
                    {
                        PostId = post.PostId,
                        UserId = post.UserId,
                        ImageUrl = post.ImageUrl,
                        Content = post.Content,
                        CreatedAt = post.CreatedAt,
                        Comments = comments,
                        User = _user
                    };
                })
                .Select(postTask => postTask.Result)
                .ToList();

            return home;
        }


    }
}
