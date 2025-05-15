namespace Ambev.DeveloperEvaluation.Common.Primitives
{
    public record struct ProductId(int Value)
    {
        public static ProductId New() => new(0);

        public override readonly string ToString() => Value.ToString();
    }
}
