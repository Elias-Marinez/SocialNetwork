
using SocialNetwork.Core.Application.Interfaces.Repository;
using SocialNetwork.Core.Domain.Entities;
using SocialNetwork.Infrastructure.Persistence.Context;

namespace SocialNetwork.Infrastructure.Persistence.Repositories
{
    public class FriendsRepository : GenericRepository<Friends>, IFriendsRepository
    {
        private readonly ApplicationContext _dbContext;

        public FriendsRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
