
using SocialNetwork.Core.Application.ViewModels.User;
using SocialNetwork.Core.Domain.Entities;

namespace SocialNetwork.Core.Application.Interfaces.Services
{
    public interface IUserService : IGenericService<UserViewModel,
                                                    UserSaveViewModel,
                                                    UserUpdateViewModel,
                                                    User>
    {

    }
}
