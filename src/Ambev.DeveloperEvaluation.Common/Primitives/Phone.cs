namespace Ambev.DeveloperEvaluation.Common.Primitives
{
    public readonly record struct Phone(string Value)
    {
        public override string ToString()
        {
            if (Value.Length == 11)
            {
                return $"({Value[..2]}) {Value[2..7]}-{Value[7..]}";
            }
            return Value;
        }
    }
}
