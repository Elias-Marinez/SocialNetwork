
using Microsoft.Extensions.DependencyInjection;
using SocialNetwork.Core.Application.Helpers;
using SocialNetwork.Core.Application.Interfaces.Helpers;
using SocialNetwork.Core.Application.Interfaces.Services;
using SocialNetwork.Core.Application.Services;
using System.Reflection;

namespace SocialNetwork.Core.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            #region AutoMapper
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            #endregion

            #region Services
            services.AddTransient(typeof(IGenericService<,,,>), typeof(GenericService<,,,>));

            services.AddTransient<IPostService, PostService>();
            services.AddTransient<ICommentService, CommentService>();
            services.AddTransient<IUserService, UserService>();

            #endregion

            #region Helpers
            services.AddScoped(typeof(IFileManager<>), typeof(FileManager<>));
            #endregion
        }
    }
}
