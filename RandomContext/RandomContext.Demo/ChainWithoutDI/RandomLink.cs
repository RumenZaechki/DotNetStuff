namespace RandomContext.Demo.ChainWithoutDI;

public class RandomLink : IChain
{
    private readonly IChain _next;
    public RandomLink(IChain next)
    {
        _next = next;
    }
    public async Task DoAsync(RandomRequest request, CancellationToken token)
    {
        AmbientContext<RandomAmbientContext>.Data = new RandomAmbientContext()
        {
            RandomStr = "Random String"
        };
        System.Console.WriteLine($"Initialized ambient context to {AmbientContext<RandomAmbientContext>.Data.RandomStr}");
        await _next.DoAsync(request, token);
    }
}
