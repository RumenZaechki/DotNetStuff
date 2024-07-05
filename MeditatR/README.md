# RandomMediator

A simple library written in .NET 8 which helps in implementing the Mediator pattern.

## Installation

Install with NuGet:

```
Install-Package RandomMediator
```

or with .NET CLI:

```
dotnet add package RandomMediator
```

## Using the package

The [MeditatR.Api](./MeditatR.Api) folder contains a demo implementation using the Mediator pattern with RandomMediator.

To add the mediator, use the `.AddMeditatR("<YOUR_NAMESPACE_HERE>")` method in `ConfigureServices` 
in `Startup` or in `Program` with `WebApplicationBuilder.Services.AddMeditatR("<YOUR_NAMESPACE_HERE>");`:

```c#
public void ConfigureServices(IServiceCollection services)
{
    services.AddMeditatR("<YOUR_NAMESPACE_HERE>");
}
```

### Example of using the mediator in the controller

```c#
public sealed class Controller : ControllerBase
{
    private readonly IMediator _mediator;

    public Controller(IMediator mediator)
    {
        _mediator = mediator;
    }
}
```
___
**This project is made for educational purposes only!**
___