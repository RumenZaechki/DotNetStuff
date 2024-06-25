using ChainOfIrresponsibility.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace ChainOfIrresponsibility.Configuration.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IChainBuilder<T> AddChain<T>(this IServiceCollection services) where T : class
        {
            IChainBuilder<T> builder = new ChainBuilder<T>(services);

            services.AddSingleton(builder);
            services.AddTransient(_ => builder.Build());

            return builder;
        }
    }
}
