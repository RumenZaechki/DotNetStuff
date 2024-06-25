namespace Demo.Chain
{
    public class RandomSuccessor : IChain
    {
        private IChain _next;
        public RandomSuccessor(IChain next)
        {
            _next = next;
        }
        public async Task ExecuteAsync(RandomRequest request, CancellationToken token)
        {
            await _next.ExecuteAsync(request, token);
            await Console.Out.WriteLineAsync("RandomSuccessor");
        }
    }
}
