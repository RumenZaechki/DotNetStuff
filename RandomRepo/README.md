# RandomRepo

A simple library written in .NET 8 which helps in implementing the Repository pattern.

## Installation

Install with NuGet:

```
Install-Package RandomRepo
```

or with .NET CLI:

```
dotnet add package RandomRepo
```

## Using the package

The [RandomRepo.Tests](./RandomRepo.Tests/TestData/) folder contains an example of how the package can be implemented.

For any data set that you'll make queries for, you'll need to make an interface and a class with the proper hierarchy: 

``` c#
    public interface IUserRepository : IRepository<User>
    {
    }

    public class UserRepository : Repository<User, DemoDbContext>, IUserRepository
    {
        public UserRepository(DemoDbContext dbContext) : base(dbContext)
        {
        }
    }
```

The class also inherits the `Repository` abstract class, which uses the given `DbContext` to make queries. The newly added interface and class should be registered in the DI container. Once this has been done, you could inject it in whatever class you need and make queries with it: 

``` c#
public class UserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public User GetUserByName(string name)
    {
        return _userRepository.GetFirst(u => u.Username == name);
    }
}
```

___
**This project is made for educational purposes only!**
___