namespace Ambev.DeveloperEvaluation.Common.Errors;

//public record struct Error(string? Title = null, string? Description = null, params object[]? Args)
//{
//    public static readonly Error None = new(string.Empty, string.Empty);
//    public static readonly Error UnauthorizedAccessCredentialsError = new("Invalid credentials", "Invalid credentials inserted");
//    public static readonly Error UnauthorizedAccessUserInactiveError = new("User inactive", "User is not active");
//    public static readonly Error UserIdRequiredError = new("Required User ID", "User ID is required");
//    public static readonly Error UserNotFoundError = new("User not found", "User ID is required", Args);
//}

public readonly record struct Error(string? Title = null, string? Description = null, params object[]? Args)
{
    public static readonly Error None = new(string.Empty, string.Empty);
    public static readonly Error UnauthorizedAccessCredentialsError = new("Invalid credentials", "Invalid credentials inserted");
    public static readonly Error UnauthorizedAccessUserInactiveError = new("User inactive", "User is not active");
    public static readonly Error UserIdRequiredError = new("Required User ID", "User ID is required");

    public static Error UserNotFoundError(int userId) => new("User not found", $"User with ID '{userId}' was not found", userId);
}
