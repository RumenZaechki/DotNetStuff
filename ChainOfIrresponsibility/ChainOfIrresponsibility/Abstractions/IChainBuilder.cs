namespace ChainOfIrresponsibility.Abstractions
{
    public interface IChainBuilder<T> where T : class
    {
        IChainBuilder<T> WithLink<TLink>() where TLink : T;
        IChainBuilder<T> WithLink(Type type);
        IChainBuilder<T> WithLink(Func<T, T> linkInstantiationFactory);
        IChainBuilder<T> WithLinks(IEnumerable<Type> types);
        T Build();
    }
}
