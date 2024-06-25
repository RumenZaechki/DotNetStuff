using Demo.Chain;
using Microsoft.Extensions.Hosting;

namespace Demo
{
    public class Worker : BackgroundService
    {
        private readonly IChain _chain;
        public Worker(IChain chain)
        {
            _chain = chain;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _chain.ExecuteAsync(new RandomRequest(), stoppingToken);
        }
    }
}
