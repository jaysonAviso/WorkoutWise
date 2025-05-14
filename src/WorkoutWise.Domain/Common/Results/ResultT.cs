using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutWise.Domain.Common.Results
{
    public class ResultT<T>
    {
        public bool IsSuccess { get; }

        public bool IsFailure => !IsSuccess;

        public T? Value { get; }
        public Error? ErrorMesage { get; }

        private ResultT(T value)
        {
            IsSuccess = true;
            Value = value;
            ErrorMesage = Error.None;
        }

        private ResultT(Error errorMessage)
        {
            IsSuccess = false;
            Value = default;
            ErrorMesage = errorMessage;
        }

        public static ResultT<T> Success(T value) => new(value);
        public static ResultT<T> Failure(Error error) => new ResultT<T>(error);
        public static ResultT<T> Failure(string errorMessage) => new Error(errorMessage);

        public static implicit operator ResultT<T>(Error error) => Failure(error);
    }
}
