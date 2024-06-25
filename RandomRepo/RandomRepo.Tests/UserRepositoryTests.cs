using Microsoft.EntityFrameworkCore;
using RandomRepo.Tests.TestData;
using RandomRepo.Tests.TestData.Abstractions;

namespace RandomRepo.Tests
{
    public class UserRepositoryTests
    {
        private readonly DemoDbContext _context;

        public UserRepositoryTests()
        {
            DbContextOptionsBuilder dbOptions = new DbContextOptionsBuilder()
                .UseInMemoryDatabase(
                    Guid.NewGuid().ToString()
            );

            _context = new DemoDbContext(dbOptions.Options);
        }

        [Fact]
        public void Add_Works_As_Expected()
        {
            // Arrange
            var repo = new UserRepository(_context);

            // Act
            var result = repo.Add(new User()
            {
                Id = 1,
                Username = "Test",
            });

            // Assert
            var user = _context.Users.FirstOrDefault(u => u.Username == "Test");
            Assert.True(result == user);
            Assert.Single(_context.Users);
        }

        [Fact]
        public void AddRange_Works_As_Expected()
        {
            // Arrange
            var repo = new UserRepository(_context);

            // Act
            var result = repo.AddRange(new List<User>()
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
            });

            // Assert
            var users = _context.Users.ToArray();
            Assert.True(result.Length == 2);
            Assert.True(result.FirstOrDefault() == users.FirstOrDefault());
            Assert.True(result.LastOrDefault() == users.LastOrDefault());
        }

        [Fact]
        public void Find_Works_As_Expected()
        {
            // Arrange
            var repo = new UserRepository(_context);

            // Act
            var result = repo.Add(new User()
            {
                Id = 1,
                Username = "Test",
            });

            // Assert
            Assert.Equal(repo.Find(u => u.Id == 1), _context.Users.Where(u => u.Id == 1));
        }

        [Fact]
        public void GetSingle_Works_As_Expected()
        {
            // Arrange
            var repo = new UserRepository(_context);

            // Act
            var result = repo.Add(new User()
            {
                Id = 1,
                Username = "Test",
            });

            // Assert
            Assert.Equal(repo.GetSingle(u => u.Id == 1), _context.Users.FirstOrDefault(u => u.Id == 1));
        }

        [Fact]
        public void GetFirst_Works_As_Expected()
        {
            // Arrange
            var repo = new UserRepository(_context);

            // Act
            var result = repo.Add(new User()
            {
                Id = 1,
                Username = "Test",
            });

            // Assert
            Assert.Equal(repo.GetFirst(u => u.Id == 1), _context.Users.First(u => u.Id == 1));
        }

        [Fact]
        public void Update_Works_As_Expected()
        {
            // Arrange
            var repo = new UserRepository(_context);
            var result = repo.Add(new User()
            {
                Id = 1,
                Username = "Test",
            });

            // Act
            result.Username = "Test1234";
            repo.Update(result);

            // Assert
            Assert.Equal("Test1234", _context.Users.FirstOrDefault().Username);
        }

        [Fact]
        public void UpdateRange_Works_As_Expected()
        {
            // Arrange
            var repo = new UserRepository(_context);
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
            var result = repo.AddRange(users);

            // Act
            result.FirstOrDefault().Username = "Test1234";
            result.LastOrDefault().Username = "Test5678";
            repo.UpdateRange(result);

            // Assert
            Assert.Equal("Test1234", repo.GetFirst(u => u.Id == 1).Username);
            Assert.Equal("Test5678", repo.GetFirst(u => u.Id == 2).Username);
        }

        [Fact]
        public void Remove_Works_As_Expected()
        {
            // Arrange
            var repo = new UserRepository(_context);
            var result = repo.Add(new User()
            {
                Id = 1,
                Username = "Test",
            });

            // Act
            var res = repo.Remove(result);

            // Assert
            Assert.True(res);
            Assert.Equal(0, _context.Users.Count());
        }

        [Fact]
        public void RemoveRange_Works_As_Expected()
        {
            // Arrange
            var repo = new UserRepository(_context);
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
            var result = repo.AddRange(users);

            // Act
            var res = repo.RemoveRange(result);

            // Assert
            Assert.True(res);
            Assert.Equal(0, _context.Users.Count());
        }
    }
}
