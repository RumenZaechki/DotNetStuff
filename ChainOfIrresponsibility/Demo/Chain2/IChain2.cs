namespace Demo.Chain2
{
    public interface IChain2
    {
        public Task DoAsync(AnotherRandomRequest request, CancellationToken token);
    }
}
