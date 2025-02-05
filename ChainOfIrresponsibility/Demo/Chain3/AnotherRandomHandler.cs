using System;

namespace Demo.Chain3;

public class AnotherRandomHandler : IChain3
{
    public Task ExecuteAsync(YetAnotherRandomRequest request, CancellationToken token)
    {
        Console.WriteLine("AnotherRandomHandler");
        return Task.CompletedTask;
    }
}
