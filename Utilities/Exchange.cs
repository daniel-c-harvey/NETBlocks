namespace NetBlocks.Utilities;

public class Exchange<TArgs, TResult> : ExchangeBase<TArgs, TResult>
{
    public delegate void RequestDelegate(TArgs args);
    public event RequestDelegate? Request;
    
    protected override void HandleRequest(TArgs args)
    {
        Request?.Invoke(args);
    }
}