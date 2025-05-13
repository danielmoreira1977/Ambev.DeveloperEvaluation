namespace Ambev.DeveloperEvaluation.Common.Primitives
{
    public readonly record struct Address(string? City, string? Street, string? Number, ZipCode? ZipCode, Geolocation? Geolocation)
    {
        public override string ToString()
        {
            var text = $"{Street} {Number}";
            text += string.IsNullOrEmpty(City) ? $", {City}" : string.Empty;
            text += ZipCode is not null ? $" - {ZipCode}" : string.Empty;
            text += Geolocation is not null ? $" | {Geolocation}" : string.Empty;

            return text;
        }
    }
}
