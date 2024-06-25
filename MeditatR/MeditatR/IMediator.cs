namespace MeditatR
{
    public interface IMediator
    {
        Task SendAsync<TRequest>(TRequest request, CancellationToken token);

        Task<TResponse> SendAsync<TRequest, TResponse>(TRequest request, CancellationToken token);
    }
}
