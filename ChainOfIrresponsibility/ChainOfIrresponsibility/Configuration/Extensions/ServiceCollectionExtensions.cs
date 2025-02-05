using ChainOfIrresponsibility.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace ChainOfIrresponsibility.Configuration.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IChainBuilder<T> AddChain<T>(this IServiceCollection services, 
        ServiceLifetime chainServiceLifetime = ServiceLifetime.Scoped) 
        where T : class
        {
            IChainBuilder<T> builder = new ChainBuilder<T>(services);
            
            services.Add(new ServiceDescriptor(typeof(IChainBuilder<T>), _ => builder,  ServiceLifetime.Singleton));
            services.Add(new ServiceDescriptor(typeof(T), _ => builder.Build(),  chainServiceLifetime));

            return builder;
        }
    }
}
