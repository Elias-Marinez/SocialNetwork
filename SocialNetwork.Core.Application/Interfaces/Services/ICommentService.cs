
using SocialNetwork.Core.Application.ViewModels.Comment;

namespace SocialNetwork.Core.Application.Interfaces.Services
{
    public interface ICommentService
    {
        Task Add(CommentSaveViewModel vm);
    }
}
