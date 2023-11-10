
using AutoMapper;
using SocialNetwork.Core.Application.Interfaces.Repository;
using SocialNetwork.Core.Application.Interfaces.Services;

namespace SocialNetwork.Core.Application.Services
{
    public class GenericService<ViewModel, SaveViewModel, UpdateViewModel, Entity> 
        : IGenericService<ViewModel, SaveViewModel, UpdateViewModel, Entity>
                where ViewModel : class
                where SaveViewModel : class
                where UpdateViewModel : class
                where Entity : class
    {
        private readonly IGenericRepository<Entity> _repository;
        private readonly IMapper _mapper;

        public GenericService(IGenericRepository<Entity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public virtual async Task Add(SaveViewModel vm)
        {
            Entity entity = _mapper.Map<Entity>(vm);
            await _repository.AddAsync(entity);
        }
        public virtual async Task Update(UpdateViewModel vm, int id)
        {
            Entity entity = _mapper.Map<Entity>(vm);
            await _repository.UpdateAsync(entity, id);
        }

        public virtual async Task Delete(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public virtual async Task<List<ViewModel>> Get()
        {
            var entities = await _repository.GetAllAsync();
            List<ViewModel> result = _mapper.Map<List<ViewModel>>(entities);

            return result;
        }

        public virtual async Task<List<ViewModel>> GetWithAll()
        {
            var entities = await _repository.GetAllWithIncludeAsync();
            List<ViewModel> result = _mapper.Map<List<ViewModel>>(entities);

            return result;
        }

        public virtual async Task<UpdateViewModel> GetById(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            UpdateViewModel result = _mapper.Map<UpdateViewModel>(entity);

            return result;
        }
    }
}
