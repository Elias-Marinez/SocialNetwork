using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Core.Application.Dtos.Account.Response;
using SocialNetwork.Core.Application.Interfaces.Services;
using SocialNetwork.Core.Application.ViewModels.User;
using SocialNetwork.Core.Application.Helpers;
using SocialNetwork.Middlewares;
using SocialNetwork.Core.Application.Interfaces.Helpers;
using SocialNetwork.Infrastructure.Identity.Entities;

namespace SocialNetwork.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IFileManager<ApplicationUser> _fileManager;
        public UserController(IUserService userService, IFileManager<ApplicationUser> fileManager )
        {
            _userService = userService;
            _fileManager = fileManager;
        }

        [ServiceFilter(typeof(LoginAuthorize))]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [ServiceFilter(typeof(LoginAuthorize))]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            AuthenticationResponse userVm = await _userService.LoginAsync(vm);
            if (userVm != null && userVm.HasError != true)
            {
                HttpContext.Session.Set<AuthenticationResponse>("user", userVm);
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            else
            {
                vm.HasError = userVm.HasError;
                vm.Error = userVm.Error;
                return View(vm);
            }
        }

        public async Task<IActionResult> LogOut()
        {
            await _userService.SignOutAsync();
            HttpContext.Session.Remove("user");
            return RedirectToRoute(new { controller = "User", action = "Login" });
        }

        [ServiceFilter(typeof(LoginAuthorize))]
        public ActionResult Register()
        {
            return View(new UserSaveViewModel());
        }

        [ServiceFilter(typeof(LoginAuthorize))]
        [HttpPost]
        public async Task<IActionResult> Register(UserSaveViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var origin = Request.Headers["origin"];

            vm.ImageUrl = await _fileManager.Save(vm.Image);

            RegisterResponse response = await _userService.RegisterAsync(vm, origin);

            if (response.HasError)
            {
                vm.HasError = response.HasError;
                vm.Error = response.Error;
                return View(vm);
            }

            return RedirectToRoute(new { controller = "User", action = "Login" });
        }

        [ServiceFilter(typeof(LoginAuthorize))]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            string response = await _userService.ConfirmEmailAsync(userId, token);
            return View("ConfirmEmail", response);
        }
        public IActionResult AccessDenied()
        {
            return View();
        }


    }
}
