namespace Demo.Chain3;

public interface IChain3
{
    public Task ExecuteAsync (YetAnotherRandomRequest request, CancellationToken token);
}
