namespace Ambev.DeveloperEvaluation.Common.Primitives
{
    public record struct UserId(Guid Value)
    {
        public static UserId New() => new(Guid.NewGuid());
        public static UserId Empty() => new(Guid.Empty);

        public override readonly string ToString() => Value.ToString();
    }
}
