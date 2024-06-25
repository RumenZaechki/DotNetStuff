# ChainOfIrresponsibility

A simple library written in .NET 8 which helps in implementing the Chain of Responsibility pattern.

## Installation

Install with NuGet:

```
Install-Package ChainOfIrresponsibility
```

or with .NET CLI:

```
dotnet add package ChainOfIrresponsibility
```

## Using the package

The [Demo](./Demo) folder contains a demo implementation using the chain of responsibility pattern with ChainOfIrresponsibility.

Your chain should consist of objects which each have reference to the next object in the chain. Such
objects are refered to as **links** in this package.

To construct a chain, use the `.AddChain<YOUR_INTERFACE_TYPE>()` method in `ConfigureServices` 
in `Startup` to add your links in order:

```c#
public void ConfigureServices(IServiceCollection services)
{
    services
            .AddChain<IChain>()
            .WithLink<RandomSuccessor>()
            .WithLink<AnotherRandomSuccessor>()
            .WithLink<YetAnotherRandomSuccessor>();

    ...
}
```

## Setup Chain on Demand Without Dependency Injection Support
If you don't have access to a service collection or want to manually construct your chain, you can 
do so using the `ChainBuilder` class:

```c#
IChain chain = new ChainBuilder<IChain>()
                        .WithLink<TestSuccessor>()
                        .WithLink<AnotherTestSuccessor>()
                        .Build();
```



## Creating Link Classes

An example link might look like this:

```c#
public class MyFirstLink : IChain
{
    private readonly IChain _nextLink;

    public MyFirstLink(IChain nextLink)
    {
        _nextLink = nextLink;
    }

    public string DoSomething(string input)
    {
        if (input == "foo")
        {
            return "bar";
        }

        return _nextLink.DoSomething(input);
    }
}
```

note that the link's constructor takes a reference to the next link in the chain. The Chain 
framework will handle instantiating this class with the correct `nextLink` reference passed in.

### Handling Dependencies

Let's say your link class needs another dependency. Your constructor might look something like this:

```c#
 public MyFirstLink(IMyChain nextLink, IMyOtherDependency myOtherDependency)
{
    _nextLink = nextLink;
    _myOtherDependency = myOtherDependency;
}
```

If you're using the service collection injection method of creating your chain, then you just need to
make sure that an instance of `IMyOtherDependency` is added to the service collection

```c#
public void ConfigureServices(IServiceCollection services)
{
    services.AddChain<IMyChain>()
        .WithLink<MyFirstLink>()
        .WithLink<MySecondLink>()
        .WithLink<DefaultLink>();

    // Add IMyOtherDependency to services
    services.AddTransient<IMyOtherDendency, MyOtherDependency>();
    ...
}
```

if you're instantiating the chain manually, you can provide an expression to the `WithLink` Method

```c#
IMyOtherDependency dep = new MyOtherDependency();

IMyChain chain = new ChainBuilder<IMyChain>()
    .WithLink<MyFirstLink>(nextLink => new MyFirstLink(nextLink, dep))
    .WithLink<MySecondLink>()
    .WithLink<DefaultLink>()
    .Build();
```
___
**This project is made for educational purposes only!**
___