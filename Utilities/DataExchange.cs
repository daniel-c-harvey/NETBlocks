using NetBlocks.Models;

namespace NetBlocks.Utilities;

public class DataExchange<TRequestArgs, TSubmitArgs, TResult> : ExchangeBase<TRequestArgs, TResult>
{
    public Func<Task> Trigger { get; }
    private Action<TRequestArgs, Action<TSubmitArgs>> WhenRequested { get; }
    private Func<TSubmitArgs, TResult> WhenSubmitted { get; }
    
    public event Event.EventBase? Requested;
    
    public DataExchange(Func<Task> trigger, Action<TRequestArgs, Action<TSubmitArgs>> whenRequested, Func<TSubmitArgs, TResult> whenSubmitted)
    {
        Trigger = trigger;
        WhenRequested = whenRequested;
        WhenSubmitted = whenSubmitted;
    }

    public void Submit(TSubmitArgs args)
    {
        Complete(WhenSubmitted(args));
    }
    
    protected override void HandleRequest(TRequestArgs args)
    {
        WhenRequested(args, Submit);
        Requested?.Invoke();
    }
}