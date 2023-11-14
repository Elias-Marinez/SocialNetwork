
using SocialNetwork.Core.Application.ViewModels.Post;
using SocialNetwork.Core.Domain.Entities;

namespace SocialNetwork.Core.Application.Interfaces.Services
{
    public interface IPostService : IGenericService<PostViewModel,
                                                    PostSaveViewModel,
                                                    PostUpdateViewModel,
                                                    Post>
    {
        Task<HomeViewModel> GetHomeViewModel();
        Task<PostUpdateViewModel> GetUpdateViewModel(int id);
        Task Update(PostUpdateViewModel vm, int id);
    }
}
