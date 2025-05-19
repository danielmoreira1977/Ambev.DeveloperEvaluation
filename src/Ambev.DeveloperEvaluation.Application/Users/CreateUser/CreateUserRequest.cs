namespace Ambev.DeveloperEvaluation.Application.Users.CreateUser;

public record struct CreateUserRequest
    (
    string City,
    string Email,
    string Firstname,
    string Lastname,
    string Number,
    string Password,
    string Phone,
    string Role,
    string Status,
    string Street,
    string Username,
    string ZipCode,
    double Latitude,
    double Longitude
    );
