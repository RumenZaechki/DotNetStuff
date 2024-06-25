namespace Demo.Chain
{
    public class YetAnotherRandomSuccessor : IChain
    {
        public async Task ExecuteAsync(RandomRequest request, CancellationToken token)
        {
            await Console.Out.WriteLineAsync("YetAnotherRandomSuccessor");
        }
    }
}
