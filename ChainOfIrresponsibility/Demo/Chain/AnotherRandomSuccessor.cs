using Demo.Chain2;

namespace Demo.Chain
{
    public class AnotherRandomSuccessor : IChain1
    {
        private readonly IChain1 _next;

        private readonly IChain2 _otherChain;
        public AnotherRandomSuccessor(IChain1 next, IChain2 otherChain)
        {
            _next = next;
            _otherChain = otherChain;
        }
        public async Task ExecuteAsync(RandomRequest request, CancellationToken token)
        {
            await _otherChain.DoAsync(new AnotherRandomRequest(), token);
            await Console.Out.WriteLineAsync("AnotherRandomSuccessor");
            await _next.ExecuteAsync(request, token);
        }
    }
}
