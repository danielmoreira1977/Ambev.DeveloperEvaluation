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

    public string City { get; private set; }
    public Geolocation Geolocation { get; private set; }
    public string Number { get; private set; }
    public string Street { get; private set; }
    public ZipCode ZipCode { get; private set; }

    public override string ToString()
    {
        return $"{Street}, {Number}, {City} - {ZipCode} | {Geolocation}";
    }
}
