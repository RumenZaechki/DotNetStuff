namespace RandomContext;

public class AmbientContext<TContext>
{
    private static readonly AsyncLocal<TContext> _data = new AsyncLocal<TContext>();

    public static TContext Data
    {
        get => _data.Value ?? default;
        set => _data.Value = value;
    }
}