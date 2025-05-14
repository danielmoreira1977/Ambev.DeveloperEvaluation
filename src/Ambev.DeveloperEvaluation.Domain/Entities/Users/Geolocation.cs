using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.Domain.Entities.Users;

[Owned]
public class Geolocation
{
    public string Lat { get; set; } = string.Empty;
    public string Long { get; set; } = string.Empty;
}
