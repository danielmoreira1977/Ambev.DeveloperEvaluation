using Ambev.DeveloperEvaluation.Common.Errors;

namespace Ambev.DeveloperEvaluation.Common.HttpResults;

public class Result<T>
{
    private Result(bool isSuccess, Error error, T? data)
    {
        if (isSuccess && error != Error.None ||
            !isSuccess && error == Error.None)
        {
            throw new ArgumentException("Invalid error state", nameof(error));
        }

        IsSuccess = isSuccess;
        Error = error;
        Data = data;
    }

    public T? Data { get; }
    public Error Error { get; }
    public bool IsFailure => !IsSuccess;
    public bool IsSuccess { get; }

    public static Result<T> Failure(Error error)
    {
        return new Result<T>(false, error, default);
    }

    public static Result<T> Success(T data)
    {
        if (data == null)
        {
            throw new ArgumentNullException(nameof(data), "Success result must have non-null data.");
        }

        return new Result<T>(true, Error.None, data);
    }

    public static Result<T> Success()
    {
        return new Result<T>(true, Error.None, default);
    }
}
