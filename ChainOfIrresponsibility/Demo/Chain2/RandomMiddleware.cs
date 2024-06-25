namespace Demo.Chain2
{
    public class RandomMiddleware : IChain2
    {
        private readonly IChain2 _next;

        public RandomMiddleware(IChain2 next)
        {
            _next = next;
        }

        public async Task DoAsync(AnotherRandomRequest request, CancellationToken token)
        {
            await Console.Out.WriteLineAsync("RandomMiddleware");
            await _next.DoAsync(request, token);
        }
    }
}
