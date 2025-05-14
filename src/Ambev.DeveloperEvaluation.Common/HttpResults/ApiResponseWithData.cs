namespace Ambev.DeveloperEvaluation.Common.HttpResults;

public class ApiResponseWithData<T> : ApiResponse
{
    public T? Data { get; set; }
}
