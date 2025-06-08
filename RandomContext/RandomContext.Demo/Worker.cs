using RandomContext.Demo.ChainWithoutDI;
using ChainOfIrresponsibility;
using RandomContext.Demo.ChainWithDI;

namespace RandomContext.Demo;

public class Worker : BackgroundService
{
    private readonly IChain2 _chain2;
    public Worker(IChain2 chain2)
    {
        _chain2 = chain2;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var chain = new ChainBuilder<IChain>()
                .WithLink<RandomLink>()
                .WithLink<AnotherRandomLink>()
                .Build();

        System.Console.WriteLine("Starting chain without DI");        
        await chain.DoAsync(new RandomRequest(), new CancellationToken());

        System.Console.WriteLine("Starting chain with DI");
        await _chain2.ExecuteAsync(new AnotherRandomRequest(), new CancellationToken());      
    }
}
