
using RandomContext.Abstractions;

namespace RandomContext.Demo.ChainWithDI;

public class Link2 : IChain2
{
    private readonly IAmbientContext<AnotherRandomAmbientContext> _context;
    public Link2(IAmbientContext<AnotherRandomAmbientContext> context)
    {
        _context = context;
    }

    public Task ExecuteAsync(AnotherRandomRequest request, CancellationToken token)
    {
        _context.Data.RandomStr = "fas;dlkfasdfkjsdfhkjasdfhajkl";
        System.Console.WriteLine($"Current value of ambient context is {_context.Data.RandomStr}");
        return Task.CompletedTask;
    }
}
