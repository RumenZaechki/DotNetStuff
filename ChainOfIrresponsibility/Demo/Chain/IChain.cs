namespace Demo.Chain
{
    public interface IChain1
    {
        public Task ExecuteAsync(RandomRequest request, CancellationToken token);
    }
}
