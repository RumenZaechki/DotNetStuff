using Microsoft.EntityFrameworkCore;
using RandomRepo.Tests.TestData;

namespace RandomRepo.Tests
{
    public class UserAsyncRepositoryTests
    {
        private readonly DemoDbContext _context;

        public UserAsyncRepositoryTests()
        {
            DbContextOptionsBuilder dbOptions = new DbContextOptionsBuilder()
                .UseInMemoryDatabase(
                    Guid.NewGuid().ToString()
            );

            _context = new DemoDbContext(dbOptions.Options);
        }

        [Fact]
        public async Task AddAsync_Works_As_Expected()
        {
            // Arrange
            var repo = new UserAsyncRepository(_context);

            // Act
            var result = await repo.AddAsync(new User()
            {
                Id = 1,
                Username = "Test",
            }, new CancellationToken());

            // Assert
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == "Test");
            Assert.True(result == user);
            Assert.Single(_context.Users);
        }

        [Fact]
        public async Task AddRangeAsync_Works_As_Expected()
        {
            // Arrange
            var repo = new UserAsyncRepository(_context);

            // Act
            var result = await repo.AddRangeAsync(new List<User>()
            {
                new User
                {
                    Id= 1,
                    Username= "Test",
                },
                new User
                {
                    Id= 2,
                    Username = "Test2"
                }
            }, new CancellationToken());

            // Assert
            var users = await _context.Users.ToArrayAsync();
            Assert.True(result.Length == 2);
            Assert.True(result.FirstOrDefault() == users.FirstOrDefault());
            Assert.True(result.LastOrDefault() == users.LastOrDefault());
        }

        [Fact]
        public async Task FindAsync_Works_As_Expected()
        {
            // Arrange
            var repo = new UserAsyncRepository(_context);

            // Act
            var result = await repo.AddAsync(new User()
            {
                Id = 1,
                Username = "Test",
            }, new CancellationToken());

            // Assert
            Assert.Equal(await repo.FindAsync(u => u.Id == 1, new CancellationToken()), _context.Users.Where(u => u.Id == 1));
        }

        [Fact]
        public async Task GetSingleAsync_Works_As_Expected()
        {
            // Arrange
            var repo = new UserAsyncRepository(_context);

            // Act
            var result = await repo.AddAsync(new User()
            {
                Id = 1,
                Username = "Test",
            }, new CancellationToken());

            // Assert
            Assert.Equal(await repo.GetSingleAsync(u => u.Id == 1, new CancellationToken()), _context.Users.FirstOrDefault(u => u.Id == 1));
        }

        [Fact]
        public async Task GetFirstAsync_Works_As_Expected()
        {
            // Arrange
            var repo = new UserAsyncRepository(_context);

            // Act
            var result = await repo.AddAsync(new User()
            {
                Id = 1,
                Username = "Test",
            }, new CancellationToken());

            // Assert
            Assert.Equal(await repo.GetFirstAsync(u => u.Id == 1, new CancellationToken()), _context.Users.First(u => u.Id == 1));
        }

        [Fact]
        public async Task UpdateAsync_Works_As_Expected()
        {
            // Arrange
            var repo = new UserAsyncRepository(_context);
            var result = await repo.AddAsync(new User()
            {
                Id = 1,
                Username = "Test",
            }, new CancellationToken());

            // Act
            result.Username = "Test1234";
            await repo.UpdateAsync(result, new CancellationToken());

            // Assert
            Assert.Equal("Test1234", _context.Users.FirstOrDefault().Username);
        }

        [Fact]
        public async Task UpdateRangeAsync_Works_As_Expected()
        {
            // Arrange
            var repo = new UserAsyncRepository(_context);
            var users = new List<User>()
            {
                new User
                {
                    Id= 1,
                    Username= "Test",
                },
                new User
                {
                    Id= 2,
                    Username = "Test2"
                }
            };
            var result = await repo.AddRangeAsync(users, new CancellationToken());

            // Act
            result.FirstOrDefault().Username = "Test1234";
            result.LastOrDefault().Username = "Test5678";
            await repo.UpdateRangeAsync(result, new CancellationToken());

            var first = await repo.GetFirstAsync(u => u.Id == 1, new CancellationToken());
            var second = await repo.GetFirstAsync(u => u.Id == 2, new CancellationToken());

            // Assert
            Assert.Equal("Test1234", first.Username);
            Assert.Equal("Test5678", second.Username);
        }

        [Fact]
        public async Task RemoveAsync_Works_As_Expected()
        {
            // Arrange
            var repo = new UserAsyncRepository(_context);
            var result = await repo.AddAsync(new User()
            {
                Id = 1,
                Username = "Test",
            }, new CancellationToken());

            // Act
            var res = await repo.RemoveAsync(result, new CancellationToken());

            // Assert
            Assert.True(res);
            Assert.Equal(0, _context.Users.Count());
        }

        [Fact]
        public async Task RemoveRangeAsync_Works_As_Expected()
        {
            // Arrange
            var repo = new UserAsyncRepository(_context);
            var users = new List<User>()
            {
                new User
                {
                    Id= 1,
                    Username= "Test",
                },
                new User
                {
                    Id= 2,
                    Username = "Test2"
                }
            };
            var result = await repo.AddRangeAsync(users, new CancellationToken());

            // Act
            var res = await repo.RemoveRangeAsync(result, new CancellationToken());

            // Assert
            Assert.True(res);
            Assert.Equal(0, _context.Users.Count());
        }
    }
}
