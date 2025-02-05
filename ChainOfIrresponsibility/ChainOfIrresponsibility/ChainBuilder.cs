using ChainOfIrresponsibility.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ChainOfIrresponsibility
{

    public class ChainBuilder<T> : IChainBuilder<T> where T : class
    {
        private readonly Stack<Func<T, T>> _linkBuilders;
        private readonly Lazy<IServiceProvider> _serviceProvider;

        public ChainBuilder() : this(new ServiceCollection()) { }

        public ChainBuilder(IServiceCollection serviceCollection) : this(new Lazy<IServiceProvider>(serviceCollection.BuildServiceProvider)) { }

        private ChainBuilder(Lazy<IServiceProvider> serviceProvider)
        {
            _linkBuilders = new Stack<Func<T, T>>();
            _serviceProvider = serviceProvider;
        }


        /// <summary>
        /// Adds a new link of type <typeparamref name="TLink"/> to the chain.
        /// </summary>
        /// <typeparam name="TLink">The type of the chain link.</typeparam>
        /// <returns>The updated chain builder.</returns>

        public IChainBuilder<T> WithLink<TLink>() where TLink : T
        {
            return WithLink(typeof(TLink));
        }

        public IChainBuilder<T> WithLink(Type type)
        {
            return WithLink(next => BuildLink(type, next));
        }

        public IChainBuilder<T> WithLink(Func<T, T> linkInstantiationFactory)
        {
            _linkBuilders.Push(linkInstantiationFactory);
            return this;
        }

        /// <summary>
        /// Adds a collection of links of to the chain.
        /// </summary>
        /// <typeparam name="IEnumerable<Type>">The collection of types of the chain link.</typeparam>
        /// <returns>The updated chain builder.</returns>
        public IChainBuilder<T> WithLinks(IEnumerable<Type> types)
        {
            foreach (Type type in types)
            {
                WithLink(type);
            }

            return this;
        }

        public T Build()
        {
            T nextLink = null;

            foreach (Func<T, T> linkBuilder in _linkBuilders)
            {
                nextLink = linkBuilder(nextLink);
            }

            return nextLink;
        }

        private T BuildLink(Type linkType, T nextLink)
        {
            ConstructorInfo[] constructors = linkType.GetConstructors();

            if (constructors.Length > 1)
            {
                throw new InvalidOperationException($"Multiple public constructors found for type '{linkType.FullName}'.");
            }

            ConstructorInfo constructor = constructors.Single();

            object[] parameters = constructor.GetParameters().Select(parameterInfo =>
            {
                if (parameterInfo.ParameterType == typeof(T))
                {
                    return nextLink ?? throw new InvalidOperationException($"Final link of type '{linkType.FullName}' expects next link in constructor, but no link was provided."); ;
                }

                return _serviceProvider.Value.GetRequiredService(parameterInfo.ParameterType);

            }).ToArray();

            return (T)constructor.Invoke(parameters);
        }
    }
}
