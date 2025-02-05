
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

        public T Pass()
        {
            State = ResultState.Pass;
            return (T)this;
        }

        public T Fail(string message)
        {
            State = State.Merge(ResultState.Fail);
            _messages.Add(new ResultMessage(message));
            return (T)this;
        }
        
        public T Warn(string message)
        {
            State = State.Merge(ResultState.Warn);
            _messages.Add(new ResultMessage(message));
            return (T)this;
        }

        public virtual string GetMessage()
        {
            return Messages.Aggregate(string.Empty, (s, failure) 
                => (string.IsNullOrEmpty(s) ? string.Empty : s + System.Environment.NewLine) + failure.Message);   
        }

        
        public T Merge(T other)
        {
            State = State.Merge(other.State);
            _messages.AddRange(other._messages);
            return (T)this;
        }

        public T MergeInto(T other)
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

        public static TSelf CreatePassResult(TContent value)
        {
            return CreatePassResult().SetValue(value);
        }

        private TSelf SetValue(TContent? value)
        {
            Value = value;
            return (TSelf)this;
        }
    }

    public class ResultContainer<T> : ResultContainerBase<ResultContainer<T>, T>
    {
        public ResultContainer() : base() { }

        public ResultContainer(T value) : base(value) { }
        
        public class ResultContainerDto<TDto> : ResultDtoBase<ResultContainer<TDto>, ResultContainerDto<TDto>>
        {
            public TDto? Value { get; set; }
        
            public ResultContainerDto() : base() { }
        
            public ResultContainerDto(ResultContainer<TDto>? result) : base(result)
            {
                if (result != null) Value = result.Value;
            }

            public override ResultContainer<TDto> From()
            {
                var result = base.From();
                result.Value = Value;
                return result;
            }
        }
    }
}
