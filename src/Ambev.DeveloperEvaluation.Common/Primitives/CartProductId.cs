namespace Ambev.DeveloperEvaluation.Common.Primitives
{
    public record struct CartProductId(int Value)
    {
        public override readonly string ToString() => Value.ToString();
    }
}
