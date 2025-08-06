
using NetBlocks.Utilities;

namespace NetBlocks.Models
{
    public enum ResultState
    {
        Pass,
        Warn,
        Fail
    }

    public class ResultMessage
    {
        public string Message { get; set; }
        public ResultMessage(string message)
        {
            Message = message;
        }
    }
    
    public abstract class ResultBase<T> where T : ResultBase<T>, new()
    {
        public ResultState State { get; internal set; }

        public bool Success => State != ResultState.Fail;

        private List<ResultMessage> _messages = [];

        public IEnumerable<ResultMessage> Messages => _messages;

        public static T CreatePassResult() { return new T { State = ResultState.Pass }; }
        public static T CreateFailResult(string message) { return new T().Fail(message); }

        public static T CreateFailResult(string[] messages)
        {
            var result = new T();
            foreach (var message in messages)
            {
                result.Fail(message);
            }
            return result;
        }

        public virtual T Pass()
        {
            State = ResultState.Pass;
            return (T)this;
        }

        public virtual T Fail(string message)
        {
            State = State.Merge(ResultState.Fail);
            _messages.Add(new ResultMessage(message));
            return (T)this;
        }
        
        public virtual T Warn(string message)
        {
            State = State.Merge(ResultState.Warn);
            _messages.Add(new ResultMessage(message));
            return (T)this;
        }

        public virtual T Inform(string message)
        {
            State = State.Merge(ResultState.Pass);
            _messages.Add(new ResultMessage(message));
            return (T)this;
        }

        public virtual string GetMessage()
        {
            return Messages.Aggregate(string.Empty, (s, failure) 
                => (string.IsNullOrEmpty(s) ? string.Empty : s + System.Environment.NewLine) + failure.Message);   
        }

        
        public virtual T Merge(T other)
        {
            State = State.Merge(other.State);
            _messages.AddRange(other._messages);
            return (T)this;
        }

        public virtual T MergeInto(T other)
        {
            other.State = State.Merge(other.State);
            other._messages.AddRange(_messages);
            return other;
        }
        
        public static T From<TOther>(ResultBase<TOther> other) where TOther : ResultBase<TOther>, new()
        {
            var result = new T();
            result.State = other.State;
            result._messages.AddRange(other._messages);
            return result;
        }

        public abstract class ResultDtoBase<TResult, TDto> 
            where TResult : ResultBase<TResult>, new()
            where TDto : ResultDtoBase<TResult, TDto>
        {
            public ResultState State { get; set; }
            public ResultMessage[] FailureReasons { get; set; }
        
            public ResultDtoBase() { }
            public ResultDtoBase(TResult? result)
            {
                if (result is null)
                {
                    State = ResultState.Fail;
                    FailureReasons = [new ResultMessage("Result is null")];
                    return;
                }

                State = result.State;
                FailureReasons = result.Messages.ToArray();
            }
        
            public virtual TResult From()
            {
                return new TResult()
                {
                    State = State,
                    _messages = [..FailureReasons]
                };
            }
        }
    }

    public class Result : ResultBase<Result>
    {
        public class ResultDto : ResultDtoBase<Result, ResultDto>
        {
            public ResultDto() : base() { }

            public ResultDto(Result? result) : base(result) { }
        }
    }

    public class PagedResult : ResultBase<PagedResult>
    {
        public Page Page { get; private set; }

        public PagedResult()
        {
            Page = new Page(0, 0);
        }

        public PagedResult(int page, int pageSize)
        {
            Page = new Page(page, pageSize);
        }
    }

    public class ResultContainerBase<TSelf, TContent> : ResultBase<TSelf>
        where TSelf : ResultContainerBase<TSelf, TContent>, new()
    {
        public TContent? Value { get; set; }
        
        public ResultContainerBase() : base() { }

        public ResultContainerBase(TContent value) : base()
        {
            Value = value;
        }

        public static TSelf CreatePassResult(TContent? value)
        {
            return CreatePassResult().SetValue(value);
        }

        public static TSelf From<TOther>(ResultContainerBase<TOther, TContent> other)
            where TOther : ResultContainerBase<TOther, TContent>, new()
        {
            var result = ResultBase<TSelf>.From(other);
            result.Value = other.Value;
            return result;
        }
        public static TSelf From<TOther>(ResultBase<TOther> other, TContent otherContent)
            where TOther : ResultBase<TOther>, new()
        {
            var result = ResultBase<TSelf>.From(other);
            result.Value = otherContent;
            return result;
        }

        private TSelf SetValue(TContent? value)
        {
            Value = value;
            return (TSelf)this;
        }
    }
    
    public class ResultContainerDtoBase<TSelf, TResult, TDto> : ResultBase<TResult>.ResultDtoBase<TResult, TSelf>
        where TSelf : ResultContainerDtoBase<TSelf, TResult, TDto>, new()
        where TResult : ResultContainerBase<TResult, TDto>, new()
    {
        public TDto? Value { get; set; }
        
        public ResultContainerDtoBase() : base() { }
        
        public ResultContainerDtoBase(TResult? result) : base(result)
        {
            if (result != null) Value = result.Value;
        }

        public override TResult From()
        {
            var result = base.From();
            result.Value = Value;
            return result;
        }
    }

    public class ResultContainer<T> : ResultContainerBase<ResultContainer<T>, T>
    {
        public ResultContainer() : base() { }

        public ResultContainer(T value) : base(value) { }
        
        public class ResultContainerDto<TDto> : ResultContainerDtoBase<ResultContainerDto<TDto>, ResultContainer<TDto>, TDto>
        {
        
            public ResultContainerDto() : base() { }
        
            public ResultContainerDto(ResultContainer<TDto>? result) : base(result)
            {
                if (result != null) Value = result.Value;
            }
        }
    }
}
