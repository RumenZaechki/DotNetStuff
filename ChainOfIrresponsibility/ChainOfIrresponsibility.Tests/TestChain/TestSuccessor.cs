namespace ChainOfIrresponsibility.Tests.TestChain
{
    public class TestSuccessor : IChain
    {
        private readonly IChain _next;
        public TestSuccessor(IChain next)
        {
            _next = next;
        }

        public IChain Next => _next;
        public async Task HandleAsync(TestRequest request, CancellationToken token)
        {
            request.Logs.Add("logging from TestSuccessor");
            await _next.HandleAsync(request, token);
        }
    }
}
