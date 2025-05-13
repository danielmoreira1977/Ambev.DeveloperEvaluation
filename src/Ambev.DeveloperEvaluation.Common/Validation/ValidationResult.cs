using FluentValidation.Results;

namespace Ambev.DeveloperEvaluation.Common.Validation;

public class ValidationResultDetail
{
    public ValidationResultDetail()
    {
    }

    public ValidationResultDetail(ValidationResult validationResult)
    {
        IsValid = validationResult.IsValid;
        Errors = validationResult.Errors.Select(o => (ValidationErrorDetail)o);
    }

    public IEnumerable<ValidationErrorDetail> Errors { get; set; } = [];
    public bool IsValid { get; set; }
}
