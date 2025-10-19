using Shared.Results.Errors;
using Shared.Results.Exceptions;

namespace Shared.Results;

public record Result
{
    protected Result(bool isSuccess, Error error)
    {
        if (isSuccess && error != Error.None
            || !isSuccess && error == Error.None)
        {
            throw new InvalidResultErrorArgumentException();
        }
        
        IsSuccess = isSuccess;
        Error = error;
    }
    
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Error Error { get; }

    public static Result Success() => new(true, Error.None);
    public static Result Failure(Error error) => new(false, error);
    
    public static implicit operator Result(Error error) => Failure(error);
}