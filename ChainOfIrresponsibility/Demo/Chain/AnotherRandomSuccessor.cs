using Demo.Chain2;

namespace Demo.Chain
{
    public class AnotherRandomSuccessor : IChain
    {
        private readonly IChain _next;

        private readonly IChain2 _otherChain;
        public AnotherRandomSuccessor(IChain next, IChain2 otherChain)
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
