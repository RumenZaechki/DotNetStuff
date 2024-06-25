namespace MeditatR.Tests.TestData
{
    public class PingHandler : IHandler<Ping, Pong>
    {
        public Task<Pong> HandleAsync(Ping request, CancellationToken token)
        {
            return Task.FromResult(new Pong { Message = request.Message + " Pong" });
        }
    }
}
