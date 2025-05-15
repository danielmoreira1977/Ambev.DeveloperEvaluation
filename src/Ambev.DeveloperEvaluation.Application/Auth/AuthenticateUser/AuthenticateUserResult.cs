namespace Ambev.DeveloperEvaluation.Application.Auth.AuthenticateUser;

/// <summary>
/// Represents the response after authenticating a user
/// </summary>
public record struct AuthenticateUserResult
    (
    int Id,
    string Email,
    string Name,
    string Phone,
    string Role,
    string Token
    );
