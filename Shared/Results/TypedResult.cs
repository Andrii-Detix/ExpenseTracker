using Shared.Results.Errors;
using Shared.Results.Exceptions;

namespace Shared.Results;

public record Result<T> : Result
{
    protected Result(bool isSuccess, Error error, T? value) 
        : base(isSuccess, error)
    {
        if (!isSuccess && !Equals(value, default(T)))
        {
            throw new InvalidResultValueArgumentException();
        }
        
        Value = value;
    }

    public T? Value { get; }

    public static Result<T> Success(T? value) => new(true, Error.None, value);
    public new static Result<T> Failure(Error error) => new(false, error, default);
    
    public static implicit operator Result<T>(T? value) => Success(value);
    public static implicit operator Result<T>(Error error) => Failure(error);
    
    public static explicit operator T?(Result<T> result)
    {
        if (result.IsFailure)
        {
            throw new ResultIsFailureOperationException();
        }
        
        return result.Value;
    }
}