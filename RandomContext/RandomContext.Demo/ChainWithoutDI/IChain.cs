namespace RandomContext.Demo.ChainWithoutDI;

public interface IChain
{
    public Task DoAsync(RandomRequest request, CancellationToken token);
}
