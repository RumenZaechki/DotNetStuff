using ChainOfIrresponsibility.Configuration.Extensions;
using Demo.Chain;
using Demo.Chain2;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Demo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services
                            .AddChain<IChain1>()
                            .WithLink<RandomSuccessor>()
                            .WithLink<AnotherRandomSuccessor>()
                            .WithLink<YetAnotherRandomSuccessor>();

                    services
                            .AddChain<IChain2>()
                            .WithLink<RandomMiddleware>()
                            .WithLink<RandomMiddleware2>()
                            .WithLink<RandomMiddleware3>();

                    services.AddHostedService<Worker>();
                });
    }
}