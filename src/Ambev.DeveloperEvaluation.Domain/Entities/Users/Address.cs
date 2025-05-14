using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.Domain.Entities.Users;

[Owned]
public class Address
{
    public string City { get; set; } = string.Empty;
    public Geolocation Geolocation { get; set; } = new();
    public int Number { get; set; }
    public string Street { get; set; } = string.Empty;
    public ZipCode ZipCode { get; set; } = new();
}
