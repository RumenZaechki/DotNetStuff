using Microsoft.Extensions.DependencyInjection;

namespace MeditatR
{
    public class Mediator : IMediator
    {
        private readonly IServiceProvider _serviceProvider;

        public Mediator(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

        public async Task SendAsync<TRequest>(TRequest request, CancellationToken token)
        {
            var handler = _serviceProvider.GetRequiredService<IHandler<TRequest>>();

            await handler.HandleAsync(request, token);
        }

        public async Task<TResponse> SendAsync<TRequest, TResponse>(TRequest request, CancellationToken token)
        {
            var handler = _serviceProvider.GetRequiredService<IHandler<TRequest, TResponse>>();

            return await handler.HandleAsync(request, token);
        }
    }
}
