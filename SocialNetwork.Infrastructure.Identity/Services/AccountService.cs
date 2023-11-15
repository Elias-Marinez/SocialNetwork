
using Azure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using SocialNetwork.Core.Application.Dtos.Account.Request;
using SocialNetwork.Core.Application.Dtos.Account.Response;
using SocialNetwork.Core.Application.Interfaces;
using SocialNetwork.Core.Application.Interfaces.Services;
using SocialNetwork.Infrastructure.Identity.Entities;
using System.Text;

namespace SocialNetwork.Infrastructure.Identity.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailService _emailService;

        public AccountService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IEmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
        }
        public async Task<AuthenticationResponse> GetUserByUserNameAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);

            
            AuthenticationResponse response = new();

            if (user == null)
            {
                response.HasError = true;
                response.Error = $"No existen cuentas registradas con {userName}";
                return response;
            }

            response.Id = user.Id;
            response.Name = user.Name;
            response.LastName = user.LastName;
            response.Email = user.Email;
            response.UserName = user.UserName;
            response.PhoneNumber = user.PhoneNumber;
            response.ImageUrl = user.ImageUrl;
            response.IsVerified = user.EmailConfirmed;

            return response;
            
        }
        public async Task<AuthenticationResponse> GetUserAsync(string id)
        {
            AuthenticationResponse response = new();

            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                response.HasError = true;
                response.Error = $"No existe usuario registrado con {id}";
                return response;
            }

            response.Id = user.Id;
            response.Name = user.Name;
            response.LastName = user.LastName;
            response.Email = user.Email;
            response.UserName = user.UserName;
            response.PhoneNumber = user.PhoneNumber;
            response.ImageUrl = user.ImageUrl;
            response.IsVerified = user.EmailConfirmed;

            return response;
        }
        public async Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request)
        {
            AuthenticationResponse response = new();

            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
            {
                response.HasError = true;
                response.Error = $"No hay cuentas registradas con {request.UserName}";
                return response;
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = $"Credenciales invalidas para {request.UserName}";
                return response;
            }

            if (!user.EmailConfirmed)
            {
                response.HasError = true;
                response.Error = $"Cuenta no confirmada para {request.UserName}";
                return response;
            }

            response.Id = user.Id;
            response.Name = user.Name;
            response.LastName = user.LastName;
            response.Email = user.Email;
            response.UserName = user.UserName;
            response.PhoneNumber = user.PhoneNumber;
            response.ImageUrl = user.ImageUrl;
            response.IsVerified = user.EmailConfirmed;

            return response;
        }
        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }
        public async Task<RegisterResponse> RegisterUserAsync(RegisterRequest request, string origin)
        {
            RegisterResponse response = new();

            var userWithSameUserName = await _userManager.FindByNameAsync(request.UserName);
            if (userWithSameUserName != null)
            {
                response.HasError = true;
                response.Error = $"Nombre de Usuario '{request.UserName}' ya ha sido tomado.";
                return response;
            }

            var userWithSameEmail = await _userManager.FindByEmailAsync(request.Email);
            if (userWithSameEmail != null)
            {
                response.HasError = true;
                response.Error = $"Email '{request.Email}' ya ha sido registrado.";
                return response;
            }

            var user = new ApplicationUser
            {
                Email = request.Email,
                Name = request.Name,
                LastName = request.LastName,
                UserName = request.UserName,
                PhoneNumber = request.PhoneNumber,
                ImageUrl = request.ImageUrl
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                var verificationUri = await SendVerificationEmailUri(user, origin);

                await _emailService.SendAsync(new Core.Application.Dtos.Email.EmailRequest()
                {
                    To = user.Email,
                    Body = $"Por favor confirma tu cuenta visitando {verificationUri}",
                    Subject = "SocialNetwork Confirmacion de registro"
                });
            }
            else
            {
                response.HasError = true;
                response.Error = $"Ha ocurrido un error mientras se registraba el usuario";
                return response;
            }

            return response;
        }
        public async Task<string> ConfirmAccountAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return $"No hay cuentas registradas con este usuario";
            }

            token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return $"Cuenta confirmada para {user.Email}. Ya puedes entrar a la App";
            }
            else
            {
                return $"Ha ocurrido un error al validar {user.Email}.";
            }
        }
        public async Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordRequest request)
        {
            ForgotPasswordResponse response = new()
            {
                HasError = false
            };

            var user = await _userManager.FindByNameAsync(request.UserName);

            if (user == null)
            {
                response.HasError = true;
                response.Error = $"No hay cuentas registradas con {request.UserName}";
                return response;
            }

            string newPassword = GenerateRandomSecurePassword();

            var result = await _userManager.RemovePasswordAsync(user);
            if (result.Succeeded)
            {
                result = await _userManager.AddPasswordAsync(user, newPassword);
                if (result.Succeeded)
                {
                    var emailBody = $"Tu nueva contraseña es: {newPassword}";
                    await _emailService.SendAsync(new Core.Application.Dtos.Email.EmailRequest()
                    {
                        To = user.Email,
                        Body = emailBody,
                        Subject = "Reset Password"
                    });
                }
            }

            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = "Failed to reset password.";
            }

            return response;
        }
        private string GenerateRandomSecurePassword()
        {
            // Obtener las opciones de contraseña de Identity
            var passwordOptions = new PasswordOptions
            {
                RequiredLength = 12,
                RequireNonAlphanumeric = true,
                RequireLowercase = true,
                RequireUppercase = true,
                RequireDigit = true
            };

            // Caracteres permitidos basados en las opciones de contraseña de Identity
            const string allowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()-_=+";

            // Filtrar caracteres permitidos basados en las opciones de contraseña de Identity
            var allowedCharsFiltered = allowedChars
                .Where(c => (passwordOptions.RequireNonAlphanumeric || char.IsLetterOrDigit(c)) &&
                            (passwordOptions.RequireLowercase || char.IsLower(c)) &&
                            (passwordOptions.RequireUppercase || char.IsUpper(c)) &&
                            (passwordOptions.RequireDigit || char.IsDigit(c)))
                .ToArray();

            var random = new Random();
            var password = new string(Enumerable.Repeat(allowedCharsFiltered, passwordOptions.RequiredLength)
                .Select(s => s[random.Next(s.Length)]).ToArray());

            return password;
        }
        private async Task<string> SendVerificationEmailUri(ApplicationUser user, string origin)
        {
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var route = "User/ConfirmEmail";
            var Uri = new Uri(string.Concat($"{origin}/", route));
            var verificationUri = QueryHelpers.AddQueryString(Uri.ToString(), "userId", user.Id);
            verificationUri = QueryHelpers.AddQueryString(verificationUri, "token", code);

            return verificationUri;
        }
    }
}
