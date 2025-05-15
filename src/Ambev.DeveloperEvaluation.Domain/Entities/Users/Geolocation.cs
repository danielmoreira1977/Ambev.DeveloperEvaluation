using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.Domain.Entities.Users;

[Owned]
public class Geolocation
{
    public Geolocation(double lat, double longitude)
    {
        Lat = lat;
        Long = longitude;
    }

    protected Geolocation()
    { }

    public double Lat { get; init; }
    public double Long { get; init; }

    public override string ToString()
    {
        return $"GEO: Lat={Lat}/Long={Long}";
    }
}
