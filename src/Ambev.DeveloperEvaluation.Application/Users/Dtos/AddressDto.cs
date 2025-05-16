using Ambev.DeveloperEvaluation.Domain.Entities.Users;

namespace Ambev.DeveloperEvaluation.Application.Users.Dtos
{
    public sealed class AddressDto
    {
        public AddressDto(Address address)
        {
            City = address.City;
            Street = address.Street;
            Number = address.Number;
            ZipCode = address.ZipCode.Value;
            Geolocation = new GeoLocationDto(address.Geolocation.Lat, address.Geolocation.Long);
        }

        public string City { get; init; }
        public GeoLocationDto Geolocation { get; init; }
        public string Number { get; init; }
        public string Street { get; init; }
        public string ZipCode { get; init; }
    }
}
