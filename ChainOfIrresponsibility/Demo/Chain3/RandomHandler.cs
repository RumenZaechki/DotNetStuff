using System;

namespace Demo.Chain3;

public class RandomHandler : IChain3
{
    private readonly IChain3 _next;

    public RandomHandler(IChain3 next)
    {
        _next = next;
    }

    public async Task ExecuteAsync(YetAnotherRandomRequest request, CancellationToken token)
    {
        Console.WriteLine("RandomHandler");
        await _next.ExecuteAsync(request, token);
    }
}
