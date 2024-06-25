using RandomRepo.Tests.TestData.Abstractions;

namespace RandomRepo.Tests.TestData
{
    public class UserRepository : Repository<User, DemoDbContext>, IUserRepository
    {
        public UserRepository(DemoDbContext dbContext) : base(dbContext)
        {
        }
    }
}
