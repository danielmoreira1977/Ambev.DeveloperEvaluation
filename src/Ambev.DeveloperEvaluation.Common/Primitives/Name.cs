namespace Ambev.DeveloperEvaluation.Common.Primitives
{
    public readonly record struct Name(string Firstname, string Lastname)
    {
        public override string ToString()
        {
            return $"{Firstname} {Lastname}";
        }
    }
}
