using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using RandomContext.Abstractions;

namespace RandomContext.Configuration.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAmbientContext<TContext>(this IServiceCollection services)
    {
        services.TryAddSingleton<IAmbientContext<TContext>, DIAmbientContext<TContext>>();
        return services;
    }
}