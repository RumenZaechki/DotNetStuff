
namespace MeditatR.Tests.TestData
{
    public class PingEventHandler : IHandler<Ping>
    {
        public async Task HandleAsync(Ping request, CancellationToken token)
        {
            request.Message = "Ping Pong Ping";
        }
    }
}
