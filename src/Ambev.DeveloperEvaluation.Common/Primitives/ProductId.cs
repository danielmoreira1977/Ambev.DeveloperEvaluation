namespace Ambev.DeveloperEvaluation.Common.Primitives
{
    public record struct ProductId(int Value)
    {
        public override readonly string ToString() => Value.ToString();
    }
}
