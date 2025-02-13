using NetBlocks.Models;

namespace NetBlocks.Utilities;

public abstract class DataExchangeBase<TRequestArgs, TSubmitArgs, TResult> : ExchangeBase<TRequestArgs, TResult>
{
    private Action<TRequestArgs, Action<TSubmitArgs>> WhenRequested { get; }
    private Func<TSubmitArgs, TResult> WhenSubmitted { get; }
    
    public event Event.EventBase? Requested;

    protected DataExchangeBase(Action<TRequestArgs, Action<TSubmitArgs>> whenRequested, Func<TSubmitArgs, TResult> whenSubmitted)
    {
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

public class DataExchange<TRequestArgs, TSubmitArgs, TResult> : DataExchangeBase<TRequestArgs, TSubmitArgs, TResult>
{
    private readonly Func<Task> _trigger;

    public DataExchange(Func<Task> trigger, Action<TRequestArgs, Action<TSubmitArgs>> whenRequested, Func<TSubmitArgs, TResult> whenSubmitted)
        : base(whenRequested, whenSubmitted)
    {
        _trigger = trigger;
    }

    public Task ExecuteTrigger()
    {
        return _trigger();
    }
}

public class DataExchange<TChoiceArgs, TRequestArgs, TSubmitArgs, TResult> : DataExchangeBase<TRequestArgs, TSubmitArgs, TResult>
{
    private readonly Func<TChoiceArgs, Task> _trigger;

    public DataExchange(Func<TChoiceArgs, Task> trigger, Action<TRequestArgs, Action<TSubmitArgs>> whenRequested, Func<TSubmitArgs, TResult> whenSubmitted)
        : base(whenRequested, whenSubmitted)
    {
        _trigger = trigger;
    }

    public Task ExecuteTrigger(TChoiceArgs args)
    {
        return _trigger(args);
    }
}