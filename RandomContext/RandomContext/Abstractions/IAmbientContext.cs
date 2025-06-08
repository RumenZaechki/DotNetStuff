namespace RandomContext.Abstractions;

public interface IAmbientContext<TContext>
{
    TContext Data { get; set; }
}