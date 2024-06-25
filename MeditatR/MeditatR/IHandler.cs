namespace MeditatR
{
    public interface IHandler<in TRequest>
    {
        Task HandleAsync(TRequest request, CancellationToken token);
    }

    public interface IHandler<in TRequest, TResponse>
    {
        Task<TResponse> HandleAsync(TRequest request, CancellationToken token);
    }
}
