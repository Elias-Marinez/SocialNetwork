using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Core.Application.Interfaces.Helpers;
using SocialNetwork.Core.Application.Interfaces.Services;
using SocialNetwork.Core.Application.ViewModels.Comment;
using SocialNetwork.Core.Application.ViewModels.Post;
using SocialNetwork.Core.Domain.Entities;
using SocialNetwork.Middlewares;
using SocialNetwork.Models;
using System.Diagnostics;

namespace SocialNetwork.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPostService _postService;
        private readonly ICommentService _commentService;
        private readonly IFileManager<Post> _fileManager;

        public HomeController(ILogger<HomeController> logger, IPostService postService, IFileManager<Post> fileManager, ICommentService commentService)
        {
            _logger = logger;
            _postService = postService;
            _fileManager = fileManager;
            _commentService = commentService;
        }

        [ServiceFilter(typeof(AppAuthorize))]
        public async Task<IActionResult> Index()
        {
            HomeViewModel model = new HomeViewModel();
            model = await _postService.GetHomeViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Post(HomeViewModel vm)
        {
            PostSaveViewModel model = new PostSaveViewModel()
            {
                Content = vm.NewPost.Content,
                UserId = vm.NewPost.UserId
            };

            if (vm.NewPost.Image != null)
            {
                model.ImageUrl = await _fileManager.Save(vm.NewPost.Image);
            }

            await _postService.Add(model);

            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }

        [HttpPost]
        public async Task<IActionResult> Comment(HomeViewModel vm)
        {
            CommentSaveViewModel model = new CommentSaveViewModel()
            {
                PostId = vm.NewComment.PostId,
                Content = vm.NewComment.Content,
                UserId = vm.NewComment.UserId
            };

            await _commentService.Add(model);

            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }

        [ServiceFilter(typeof(AppAuthorize))]
        public async Task<ActionResult> Edit(int id)
        {
            return View(await _postService.GetUpdateViewModel(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, PostUpdateViewModel vm)
        {
            try
            {
                if (vm.Image != null)
                {
                    vm.ImageUrl = await _fileManager.Update(vm.Image, vm.ImageUrl);
                }

                await _postService.Update(vm, id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [ServiceFilter(typeof(AppAuthorize))]
        public async Task<ActionResult> Delete(int id)
        {
            return View(await _postService.GetUpdateViewModel(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, PostUpdateViewModel vm)
        {
            try
            {
                if (vm.ImageUrl != null)
                {
                    _fileManager.Delete(vm.ImageUrl);
                }

                await _postService.Delete(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}