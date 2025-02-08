namespace NetBlocks.Utilities;

public class Exchange<TArgs, TResult>
{
    private TaskCompletionSource<TResult>? _pendingRequest;

    public async Task<TResult> MakeRequest(TArgs args)
    {
        _pendingRequest = new TaskCompletionSource<TResult>();
        OnRequest(args);
        return await _pendingRequest.Task;
    }

    public void Complete(TResult result)
    {
        _pendingRequest?.TrySetResult(result);
    }
    
    public delegate void ExchangeDelegate(TArgs args);
    public event ExchangeDelegate? Request;
    
    private void OnRequest(TArgs args)
    {
        Request?.Invoke(args);
    }
}