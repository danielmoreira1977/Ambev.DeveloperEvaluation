namespace Ambev.DeveloperEvaluation.Common.Primitives
{
    public record struct ProductId(Guid Value)
    {
        public static ProductId New() => new(Guid.NewGuid());
        public static ProductId Empty() => new(Guid.Empty);

        public override readonly string ToString() => Value.ToString();
    }
}
