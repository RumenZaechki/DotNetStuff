namespace ChainOfIrresponsibility.Tests.TestChain
{
    public class AnotherTestSuccessor : IChain
    {
        public async Task HandleAsync(TestRequest request, CancellationToken token)
        {
            request.Logs.Add("logging from AnotherTestSuccessor");
        }
    }
}
