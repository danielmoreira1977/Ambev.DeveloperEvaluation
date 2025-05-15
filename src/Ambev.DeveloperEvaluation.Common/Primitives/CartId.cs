namespace Ambev.DeveloperEvaluation.Common.Primitives
{
    public record struct CartId(int Value)
    {
        public override readonly string ToString() => Value.ToString();
    }
}
