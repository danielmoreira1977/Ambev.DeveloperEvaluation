namespace Ambev.DeveloperEvaluation.Common.Primitives
{
    public record struct UserId(int Value)
    {
        public override readonly string ToString() => Value.ToString();
    }
}
