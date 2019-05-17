using ParrotWings.Models;

namespace ParrotWings.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(RepositoryContext dbContext)
        {
            DbContext = dbContext;
        }
    }
}