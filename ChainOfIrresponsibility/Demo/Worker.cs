using ChainOfIrresponsibility;
using Demo.Chain;
using Demo.Chain3;
using Microsoft.Extensions.Hosting;

namespace Demo
{
    public class Worker : BackgroundService
    {
        private readonly IChain1 _chain;
        public Worker(IChain1 chain)
        {
            _chain = chain;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _chain.ExecuteAsync(new RandomRequest(), stoppingToken);

            var chain3 = new ChainBuilder<IChain3>()
                                        .WithLinks(new List<Type>() { typeof(RandomHandler), typeof(AnotherRandomHandler) })
                                        .Build();
                                        
            await chain3.ExecuteAsync(new YetAnotherRandomRequest(), stoppingToken);
        }
    }
}
