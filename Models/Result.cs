
namespace NetBlocks.Models
{
    public enum ResultState
    {
        Pass,
        Fail
    }

    public class ResultFailure
    {
        public string Message { get; set; }
        public ResultFailure(string message)
        {
            Message = message;
        }
    }
    
    public abstract class ResultBase<T> where T : ResultBase<T>, new()
    {
        public ResultState State { get; internal set; }

        public bool Success => State == ResultState.Pass;

        internal List<ResultFailure> Failures = [];

        public IEnumerable<ResultFailure> FailureReasons => Failures;

        public static T CreatePassResult() { return new T { State = ResultState.Pass }; }
        public static T CreateFailResult(string message) { return new T().Fail(message); }

        public T Pass()
        {
            State = ResultState.Pass;
            return (T)this;
        }

        public T Fail(string message)
        {
            State = ResultState.Fail;
            Failures.Add(new ResultFailure(message));
            return (T)this;
        }

        public virtual string GetFailureMessage()
        {
            return FailureReasons.Aggregate(string.Empty, (s, failure) 
                => (string.IsNullOrEmpty(s) ? string.Empty : s + System.Environment.NewLine) + failure.Message);   
        }

        
        public T Merge(T other)
        {
            if (State is not ResultState.Fail) State = other.State;
            Failures.AddRange(other.Failures);
            return (T)this;
        }

        public T MergeInto(T other)
        {
            if (other.State is not ResultState.Fail) other.State = State;
            other.Failures.AddRange(Failures);
            return other;
        }
    }

    public class Result : ResultBase<Result> { }

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

    public class ResultContainer<T> : ResultBase<ResultContainer<T>>
    {
        public T? Value { get; set; }

        public ResultContainer() : base() { }

        public ResultContainer(T value) : base()
        {
            Value = value;
        }
    }
    
    /********* Serializable DTOs *********/
    public abstract class ResultDtoBase<TResult, TDto> 
        where TResult : ResultBase<TResult>, new()
        where TDto : ResultDtoBase<TResult, TDto>
    {
        public ResultState State { get; set; }
        public ResultFailure[] FailureReasons { get; set; }
        
        public ResultDtoBase() { }
        public ResultDtoBase(TResult? result)
        {
            if (result is null)
            {
                State = ResultState.Fail;
                FailureReasons = [new ResultFailure("Result is null")];
                return;
            }

            State = result.State;
            FailureReasons = result.FailureReasons.ToArray();
        }
        
        public virtual TResult From()
        {
            return new TResult()
            {
                State = State,
                Failures = [..FailureReasons]
            };
        }
    }

    public class ResultDto : ResultDtoBase<Result, ResultDto>
    {
        public ResultDto() : base() { }

        public ResultDto(Result? result) : base(result) { }
    }

    public class ResultContainerDto<T> : ResultDtoBase<ResultContainer<T>, ResultContainerDto<T>>
    {
        public T? Value { get; set; }
        
        public ResultContainerDto() : base() { }
        
        public ResultContainerDto(ResultContainer<T>? result, T? value) : base(result)
        {
            Value = value;
        }


        public override ResultContainer<T> From()
        {
            var result = base.From();
            result.Value = Value;
            return result;
        }
    }
}
