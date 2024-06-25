namespace ChainOfIrresponsibility.Tests.TestChain
{
    public interface IChain
    {
        public Task HandleAsync(TestRequest request, CancellationToken token);
    }
}
