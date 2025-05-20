using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.Domain.Entities.Users;

[Owned]
public class Address
{
    public Address(string city, Geolocation geolocation, string number, string street, ZipCode zipCode)
    {
        City = city;
        Geolocation = geolocation;
        Number = number;
        Street = street;
        ZipCode = zipCode;
    }

    protected Address()
    { }

    public string? City { get; init; }
    public Geolocation? Geolocation { get; init; }
    public string? Number { get; init; }
    public string? Street { get; init; }
    public ZipCode? ZipCode { get; init; }

    public override string ToString()
    {
        return $"{Street}, {Number}, {City} - {ZipCode} | {Geolocation}";
    }
}
