using PS.Core.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PS.Core.Concrete
{
    public class Result : IResult
    {
        public Result()
        {

        }

        public string[] Errors { get; set; }
        public bool IsSucceeded { get { return HttpStatusCode >= 200 && HttpStatusCode < 300; } }
        public int? HttpStatusCode { get; set; }
        public string ResponseCode { get; set; }
        public string CorrelationId { get; set; }

        public Result(IEnumerable<string> errors)
        {
            Errors = errors.ToArray();
        }

        public Result<T> SetSuccess<T>(int statusCode = 200, string responseCode = null)
        {
            HttpStatusCode = statusCode;
            ResponseCode = responseCode;

            return (Result<T>)this;
        }

        public Result<T> SetFailed<T>(int statusCode = 400, string responseCode = null)
        {
            HttpStatusCode = statusCode;
            ResponseCode = responseCode;

            return (Result<T>)this;
        }

        public static Result Success()
        {
            return new Result(Array.Empty<string>());
        }
        public static Task<Result> SuccessAsync()
        {
            return Task.FromResult(new Result(Array.Empty<string>()));
        }
        public static Result Failure(IEnumerable<string> errors)
        {
            return new Result(errors);
        }
        public static Task<Result> FailureAsync(IEnumerable<string> errors)
        {
            return Task.FromResult(new Result(errors));
        }
    }
    public class Result<T> : Result, IResult<T>
    {
        /// <summary>
        /// Contains the result of the process.
        /// </summary>
        /// <example>1</example>
        public T Data { get; set; }

        public static new Result<T> Failure(IEnumerable<string> errors)
        {
            return new Result<T> { Errors = errors.ToArray() };
        }
        public static new async Task<Result<T>> FailureAsync(IEnumerable<string> errors)
        {
            return await Task.FromResult(Failure(errors));
        }
        public static Result<T> Success(T data)
        {
            return new Result<T> { Data = data };
        }
        public static async Task<Result<T>> SuccessAsync(T data)
        {
            return await Task.FromResult(Success(data));
        }
    }
}
