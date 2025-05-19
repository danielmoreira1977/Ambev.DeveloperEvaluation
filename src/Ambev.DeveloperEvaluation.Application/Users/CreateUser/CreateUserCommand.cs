using Ambev.DeveloperEvaluation.Common.HttpResults;
using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Users.CreateUser;

/// <summary>
/// Command for creating a new user.
/// </summary>
/// <remarks>
/// This command is used to capture the required data for creating a user, including username,
/// password, phone number, email, status, and role. It implements <see cref="IRequest{TResponse}"/>
/// to initiate the request that returns a <see cref="CreateUserResult"/>.
///
/// The data provided in this command is validated using the <see
/// cref="CreateUserCommandValidator"/> which extends <see cref="AbstractValidator{T}"/> to ensure
/// that the fields are correctly populated and follow the required rules.
/// </remarks>
public class CreateUserCommand
    (
    string city,
    string email,
    string firstname,
    string lastname,
    string number,
    string password,
    string phone,
    string role,
    string status,
    string street,
    string username,
    string zipCode,
    double latitude,
    double longitude
    ) : IRequest<Result<CreateUserResult>>
{
    public string City { get; init; } = city;
    public string? Email { get; init; } = email;
    public string Firstname { get; init; } = firstname;
    public string Lastname { get; init; } = lastname;
    public double Latitude { get; init; } = latitude;
    public double Longitude { get; init; } = longitude;
    public string Number { get; init; } = number;
    public string? Password { get; init; } = password;

    public string? Phone { get; init; } = phone;

    public string? Role { get; init; } = role;

    public string? Status { get; init; } = status;
    public string Street { get; } = street;
    public string? Username { get; init; } = username;
    public string ZipCode { get; init; } = zipCode;

    public ValidationResultDetail Validate()
    {
        var validator = new CreateUserCommandValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}
