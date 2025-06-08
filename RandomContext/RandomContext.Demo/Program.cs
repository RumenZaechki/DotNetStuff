using RandomContext.Demo;
using RandomContext.Demo.ChainWithDI;
using RandomContext.Configuration.Extensions;
using ChainOfIrresponsibility.Configuration.Extensions;

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
                .AddChain<IChain2>(ServiceLifetime.Singleton)
                .WithLink<Link1>()
                .WithLink<Link2>();

            services.AddAmbientContext<AnotherRandomAmbientContext>();
            services.AddHostedService<Worker>();
            });
}