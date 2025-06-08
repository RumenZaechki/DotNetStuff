namespace Demo.Chain
{
    public class RandomSuccessor : IChain1
    {
        private IChain1 _next;
        public RandomSuccessor(IChain1 next)
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
