namespace Ambev.DeveloperEvaluation.Common.Primitives
{
    public record struct UserId(int Value)
    {
        public static UserId New() => new(0);

        public override readonly string ToString() => Value.ToString();
    }
}
