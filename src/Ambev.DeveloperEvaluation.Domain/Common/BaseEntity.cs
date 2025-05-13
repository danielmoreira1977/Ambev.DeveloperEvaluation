using Ambev.DeveloperEvaluation.Common.Validation;

namespace Ambev.DeveloperEvaluation.Domain.Common;

public class BaseEntity<Tid> : IComparable<BaseEntity<Tid>>
{
    public Tid Id { get; protected init; }

    public int CompareTo(BaseEntity<Tid>? other)
    {
        if (other == null)
        {
            return 1;
        }

        if (Id is IComparable comparableId && other.Id is IComparable comparableOtherId)
        {
            return comparableId.CompareTo(comparableOtherId);
        }

        throw new InvalidOperationException("Id does not implement IComparable.");
    }

    public Task<IEnumerable<ValidationErrorDetail>> ValidateAsync()
    {
        return Validator.ValidateAsync(this);
    }
}
