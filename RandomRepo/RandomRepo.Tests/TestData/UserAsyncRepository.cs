using RandomRepo.Async;
using RandomRepo.Tests.TestData.Abstractions;

namespace RandomRepo.Tests.TestData
{
    public class UserAsyncRepository : AsyncRepository<User, DemoDbContext>, IUserAsyncRepository
    {
        public UserAsyncRepository(DemoDbContext dbContext) : base(dbContext)
        {
        }
    }
}
