namespace RandomContext.Demo.ChainWithDI;

public interface IChain2
{
    Task ExecuteAsync(AnotherRandomRequest request, CancellationToken token);
}
