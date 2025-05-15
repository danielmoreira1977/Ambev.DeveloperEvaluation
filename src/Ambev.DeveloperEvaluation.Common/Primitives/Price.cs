namespace Ambev.DeveloperEvaluation.Common.Primitives
{
    public record struct Price(decimal Value)
    {
        public static Price Empty() => new(0m);
    }
}
