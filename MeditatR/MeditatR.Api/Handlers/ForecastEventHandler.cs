
namespace MeditatR.Api
{
    public class ForecastEventHandler : IHandler<string>
    {
        public async Task HandleAsync(string request, CancellationToken token)
        {
            Console.WriteLine("it works!");
        }
    }
}
