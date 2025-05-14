using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutWise.Domain.Common.Results
{
    public sealed class Result
    {
        public bool IsSuccess { get; }

        public bool IsFailure => !IsSuccess;
        public Error? Error { get; }

        private Result()
        {
            IsSuccess = true;
            Error = Error.None;
        }

        private Result(Error error)
        {
            IsSuccess = false;
            Error = error;
        }

        public static Result Success() => new();

        public static Result Failure(Error error) => new(error);

        public static Result Failure(string message) => new(new Error(message));

        public override string ToString() =>
            IsSuccess ? "Success" : $"Failure: {Error}";
    }
}
