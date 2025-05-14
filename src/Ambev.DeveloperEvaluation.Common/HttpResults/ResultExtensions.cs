using Ambev.DeveloperEvaluation.Common.Errors;

namespace Ambev.DeveloperEvaluation.Common.HttpResults;

public static class ResultExtensions
{
    public static TResult Match<T, TResult>(
        this Result<T> result,
        Func<T, TResult> onSuccess,
        Func<Error, TResult> onFailure)
    {
        return result.IsSuccess
            ? onSuccess(result.Data!)
            : onFailure(result.Error);
    }
}
