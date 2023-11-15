
using AutoMapper;
using Microsoft.AspNetCore.Http;
using SocialNetwork.Core.Application.Dtos.Account.Response;
using SocialNetwork.Core.Application.Helpers;
using SocialNetwork.Core.Application.Interfaces.Repository;
using SocialNetwork.Core.Application.Interfaces.Services;
using SocialNetwork.Core.Application.ViewModels.Comment;
using SocialNetwork.Core.Application.ViewModels.Friends;
using SocialNetwork.Core.Application.ViewModels.Post;
using SocialNetwork.Core.Domain.Entities;

namespace SocialNetwork.Core.Application.Services
{
    public class FriendsService : GenericService<FriendsViewModel, FriendsSaveViewModel, FriendsViewModel, Friends>,  IFriendsService
    {
        private readonly IFriendsRepository _repository;
        private readonly IAccountService _accountService;
        private readonly IPostService _postService;
        private readonly AuthenticationResponse _user;
        private readonly IMapper _mapper;

        public FriendsService(IFriendsRepository repository, IMapper mapper, IAccountService accountService, IPostService postService, IHttpContextAccessor httpContextAccessor ) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _accountService = accountService;
            _postService = postService;
            _user = httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");

        }

        public override async Task<FriendsSaveViewModel> Add(FriendsSaveViewModel vm)
        {
            AuthenticationResponse? response = await _accountService.GetUserByUserNameAsync(vm.FriendUserName);

            FriendsSaveViewModel friendsSave = new FriendsSaveViewModel();

            if(response.HasError)
            {
                friendsSave.HasError = response.HasError;
                friendsSave.Error = response.Error;
            }
            else
            {
                vm.FriendId = response.Id;
                await base.Add(vm);
                friendsSave.HasError = response.HasError;
                friendsSave.Error = response.Error;
            }
            return friendsSave;
        }
        public async Task<FriendsHomeViewModel> GetFriendsViewModel()
        {
            FriendsHomeViewModel vm = new FriendsHomeViewModel()
            {
                User = _user,
                Posts = await this.GetFriendsPosts(_user.Id),
                Friends = await this.GetFriends()

            };
            return vm;
        }
        private async Task<IEnumerable<FriendViewModel>> GetFriends()
        {
            var friends = await base.Get();
            var userFriends= await Task.WhenAll(friends
                                .Where(f => f.UserId == _user.Id)
                                .Select(async friend =>
                                {
                                    var user = await _accountService.GetUserAsync(friend.FriendId);
                                    return new FriendViewModel()
                                    {
                                        Id = user.Id,
                                        Name = user.Name,
                                        LastName = user.LastName,
                                        Username = user.UserName,
                                        FriendsId = friend.FriendsId
                                    };
                                }));
            
            return userFriends;
        }
        private async Task<IEnumerable<PostViewModel>> GetFriendsPosts(string UserId)
        {
            var friends = await base.Get();
            var friendUserIds = friends.Where(f => f.UserId == _user.Id).Select(f => f.FriendId).ToList();

            var posts = await _postService.GetWithAll();

            var userPosts = new List<PostViewModel>();

            foreach (var post in posts.Where(post => friendUserIds.Contains(post.UserId))
                                      .OrderByDescending(post => post.CreatedAt))
            {
                var comments = _mapper.Map<List<CommentViewModel>>(post.Comments);

                foreach (var comment in comments)
                {
                    comment.User = await _accountService.GetUserAsync(comment.UserId);
                }

                var postViewModel = new PostViewModel
                {
                    PostId = post.PostId,
                    UserId = post.UserId,
                    ImageUrl = post.ImageUrl,
                    Content = post.Content,
                    CreatedAt = post.CreatedAt,
                    Comments = comments,
                    User = await _accountService.GetUserAsync(post.UserId)
                };

                userPosts.Add(postViewModel);
            }

            return userPosts;
        }
    }
}
