namespace Ambev.DeveloperEvaluation.Common.Primitives;

public readonly record struct Geolocation(string Lat, string Long)
{
    public override string ToString()
    {
        return $"Latitude: {Lat} - Longitude: {Long}";
    }
}
