using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Core.Application.Interfaces.Services;
using SocialNetwork.Core.Application.ViewModels.Comment;
using SocialNetwork.Core.Application.ViewModels.Friends;
using SocialNetwork.Middlewares;

namespace SocialNetwork.Controllers
{
    public class FriendsController : Controller
    {
        private readonly IFriendsService _friendsService;
        private readonly ICommentService _commentService;

        public FriendsController(IFriendsService friendsService, ICommentService commentService)
        {
            _friendsService = friendsService;
            _commentService = commentService;
        }

        [ServiceFilter(typeof(AppAuthorize))]
        public async Task<IActionResult> Index()
        {
            FriendsHomeViewModel vm = new FriendsHomeViewModel();
            vm = await _friendsService.GetFriendsViewModel();
            return View(vm);
        }

        [ServiceFilter(typeof(AppAuthorize))]
        [HttpPost]
        public async Task<IActionResult> Friend(FriendsHomeViewModel vm)
        {
            FriendsSaveViewModel model = new FriendsSaveViewModel()
            {
                UserId = vm.NewFriend.UserId,
                FriendUserName = vm.NewFriend.FriendUserName
            };

            await _friendsService.Add(model);

            return RedirectToRoute(new { controller = "Friends", action = "Index" });
        }

        [ServiceFilter(typeof(AppAuthorize))]
        [HttpPost]
        public async Task<IActionResult> Comment(FriendsHomeViewModel vm)
        {
            CommentSaveViewModel model = new CommentSaveViewModel()
            {
                PostId = vm.NewComment.PostId,
                Content = vm.NewComment.Content,
                UserId = vm.NewComment.UserId
            };

            await _commentService.Add(model);

            return RedirectToRoute(new { controller = "Friends", action = "Index" });
        }

        [ServiceFilter(typeof(AppAuthorize))]
        public async Task<IActionResult> Delete(int id)
        {

            await _friendsService.Delete(id);

            return RedirectToRoute(new { controller = "Friends", action = "Index" });
        }
    }
}
