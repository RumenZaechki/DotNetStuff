namespace Demo.Chain2
{
    public class RandomMiddleware3 : IChain2
    {
        public async Task DoAsync(AnotherRandomRequest request, CancellationToken token)
        {
            await Console.Out.WriteLineAsync("RandomMiddleware3");
        }
    }
}
