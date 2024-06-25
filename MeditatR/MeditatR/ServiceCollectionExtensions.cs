using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
using System.Reflection;

namespace MeditatR
{
    public static class ServiceCollectionExtensions
    {
        public static void AddMeditatR(this IServiceCollection services, string @namespace)
        {
            services.AddScoped<IMediator, Mediator>();

            var assemblies = DependencyContext.Default!.GetDefaultAssemblyNames().Where(assembly => assembly.FullName.StartsWith(@namespace)).Select(Assembly.Load);

            var types = assemblies.SelectMany(assembly => assembly.GetTypes()).ToList();

            types.Where(type => type.GetInterfaces().Any(IsHandler)).ToList().ForEach(type => type.GetInterfaces().Where(IsHandler).ToList().ForEach(@interface => services.AddScoped(@interface, type)));

            return;

        }
        static bool IsHandler(Type type) => IsType(type, typeof(IHandler<>)) || IsType(type, typeof(IHandler<,>));

        static bool IsType(Type type, MemberInfo memberInfo) => type is not null && type.IsGenericType && type.GetGenericTypeDefinition() == memberInfo;
    }
}
