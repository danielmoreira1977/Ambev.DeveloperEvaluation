namespace Ambev.DeveloperEvaluation.Common.Primitives
{
    public record struct CartId(int Value)
    {
        public static CartId New() => new(0);

        public override readonly string ToString() => Value.ToString();
    }
}
