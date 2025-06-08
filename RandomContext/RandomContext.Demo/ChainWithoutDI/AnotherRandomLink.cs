using RandomContext;

namespace RandomContext.Demo.ChainWithoutDI;

public class AnotherRandomLink : IChain
{
    public Task DoAsync(RandomRequest request, CancellationToken token)
    {
        Console.WriteLine($"Ambient Context value from another class: {AmbientContext<RandomAmbientContext>.Data.RandomStr}");
        return Task.CompletedTask;
    }
}
