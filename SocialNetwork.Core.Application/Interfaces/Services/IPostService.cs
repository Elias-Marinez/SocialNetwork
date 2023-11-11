
using SocialNetwork.Core.Application.ViewModels.Post;
using SocialNetwork.Core.Domain.Entities;

namespace SocialNetwork.Core.Application.Interfaces.Services
{
    public interface IPostService : IGenericService<PostViewModel,
                                                    PostSaveViewModel,
                                                    PostUpdateViewModel,
                                                    Post>
    {
        Task Update(PostUpdateViewModel vm, int id);
    }
}
