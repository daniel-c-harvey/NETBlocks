using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBlocks.Models
{
    public abstract class Result<T> where T : Result<T>, new()
    {
        public enum ResultState
        {
            Pass,
            Fail
        }

        public class ResultFailure
        {
            public string Message { get; private set; }
            public ResultFailure(string message)
            {
                Message = message;
            }
        }

        public ResultState State { get; private set; }

        public bool Success => State == ResultState.Pass;

        protected List<ResultFailure> failures = new List<ResultFailure>();
        public IEnumerable<ResultFailure> FailureReasons => failures;

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
            failures.Add(new ResultFailure(message));
            return (T)this;
        }

    }

    public class Result : Result<Result> { /* type definition */ }

    public class PagedResult : Result<PagedResult>
    {
        public Page Page { get; private set; }

        public PagedResult()
        {
            Page = new(0, 0);
        }

        public PagedResult(int page, int pageSize)
        {
            Page = new(page, pageSize);
        }
    }

    public class ResultContainer<T> : Result<ResultContainer<T>>
    {
        public T Value { get; set; }

        public ResultContainer() : base() { }

        public ResultContainer(T value) : base()
        {
            Value = value;
        }
    }
}
