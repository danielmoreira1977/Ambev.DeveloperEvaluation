using Ambev.DeveloperEvaluation.Domain.Entities.Users;

namespace Ambev.DeveloperEvaluation.Application.Users.Dtos
{
    public class UserDto
    {
        public UserDto(User user)
        {
            Id = user.Id.Value;
            Email = user.Email.Value;
            Username = user.Username.Value;
            Password = user.Password.Value;
            Name = new NameDto(user.Name!.Firstname, user.Name.Lastname);
            Address = new AddressDto(user.Address);
            Phone = user.Phone.Value;
            Status = user.Status!.Name;
            Role = user.Role!.Name;
        }

        public AddressDto Address { get; init; }
        public string Email { get; init; }
        public int Id { get; init; }
        public NameDto Name { get; init; }
        public string Password { get; init; }
        public string Phone { get; init; }
        public string Role { get; init; }
        public string Status { get; init; }
        public string Username { get; init; }
    }
}
