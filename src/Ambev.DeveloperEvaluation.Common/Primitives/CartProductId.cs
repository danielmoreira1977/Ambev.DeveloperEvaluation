namespace Ambev.DeveloperEvaluation.Common.Primitives
{
    public record struct CartProductId(Guid Value)
    {
        public static CartProductId New() => new(Guid.NewGuid());
        public static CartProductId Empty() => new(Guid.Empty);

        public override readonly string ToString() => Value.ToString();
    }
}
