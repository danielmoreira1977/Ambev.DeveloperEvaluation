namespace Ambev.DeveloperEvaluation.Common.Primitives
{
    public record struct CartProductId(int Value)
    {
        public static CartProductId New() => new(0);

        public override readonly string ToString() => Value.ToString();
    }
}
