namespace Ambev.DeveloperEvaluation.Common.Primitives
{
    public record struct CartId(Guid Value)
    {
        public static CartId New() => new(Guid.NewGuid());
        public static CartId Empty() => new(Guid.Empty);

        public override readonly string ToString() => Value.ToString();
    }
}
