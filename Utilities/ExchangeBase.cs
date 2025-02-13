namespace NetBlocks.Utilities;

public abstract class ExchangeBase<TArgs, TResult>
{
    protected TaskCompletionSource<TResult>? _pendingRequest;

    public async Task<TResult> MakeRequest(TArgs args)
    {
        _pendingRequest = new TaskCompletionSource<TResult>();
        HandleRequest(args);
        return await _pendingRequest.Task;
    }

    public virtual void Complete(TResult result)
    {
        _pendingRequest?.TrySetResult(result);
    }

    protected abstract void HandleRequest(TArgs args);
}