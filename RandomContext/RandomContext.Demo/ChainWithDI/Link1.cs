using RandomContext.Abstractions;

namespace RandomContext.Demo.ChainWithDI;

public class Link1 : IChain2
{
    private readonly IChain2 _next;
    private readonly IAmbientContext<AnotherRandomAmbientContext> _context;
    public Link1(IChain2 next, IAmbientContext<AnotherRandomAmbientContext> context)
    {
        _next = next;
        _context = context;
    }
    public async Task ExecuteAsync(AnotherRandomRequest request, CancellationToken token)
    {
        _context.Data = new AnotherRandomAmbientContext
        {
            RandomStr = "Random String"
        };
        System.Console.WriteLine($"Initialized ambient context to {_context.Data.RandomStr}");
        await _next.ExecuteAsync(request, token);
    }
}
