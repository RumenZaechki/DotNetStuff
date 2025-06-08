using RandomContext.Abstractions;

namespace RandomContext;

public class DIAmbientContext<TContext> : IAmbientContext<TContext>
{
    private static readonly AsyncLocal<TContext> _data = new AsyncLocal<TContext>();

    public TContext Data
    {
        get => _data.Value ?? default;
        set => _data.Value = value;
    }
}
