namespace Ambev.DeveloperEvaluation.Common.HttpResults;

public class PaginatedResponse<T> : ApiResponseWithData<IEnumerable<T>>
{
    public int CurrentPage { get; set; }
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
}
