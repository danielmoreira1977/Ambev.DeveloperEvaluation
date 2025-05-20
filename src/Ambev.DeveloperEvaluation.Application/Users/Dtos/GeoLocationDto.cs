namespace Ambev.DeveloperEvaluation.Application.Users.Dtos
{
    public readonly record struct GeoLocationDto(
        double? Lat,
        double? Long
    );
}
