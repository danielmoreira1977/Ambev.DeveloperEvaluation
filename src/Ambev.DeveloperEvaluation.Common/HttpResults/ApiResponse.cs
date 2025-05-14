using Ambev.DeveloperEvaluation.Common.Validation;

namespace Ambev.DeveloperEvaluation.Common.HttpResults;

public class ApiResponse
{
    public IEnumerable<ValidationErrorDetail> Errors { get; set; } = [];
    public string Message { get; set; } = string.Empty;
    public bool Success { get; set; }
}
