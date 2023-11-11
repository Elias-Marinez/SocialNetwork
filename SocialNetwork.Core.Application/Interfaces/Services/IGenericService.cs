
namespace SocialNetwork.Core.Application.Interfaces.Services
{
    public interface IGenericService<ViewModel, SaveViewModel, UpdateViewModel, Entity>
                                    where ViewModel : class
                                    where SaveViewModel : class
                                    where Entity : class
    {
        Task Add(SaveViewModel vm);
        Task Delete(int id);
        Task<List<ViewModel>> Get();
        Task<UpdateViewModel> GetById(int id);
        Task<List<ViewModel>> GetWithAll();
    }
}
