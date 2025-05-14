namespace Ambev.DeveloperEvaluation.Common.Errors;

public record struct Error(string Title, string Description)
{
    public static readonly Error None = new(string.Empty, string.Empty);
    public static readonly Error UnauthorizedAccessCredentialsError = new("Invalid credentials", "Invalid credentials inserted");
    public static readonly Error UnauthorizedAccessUserInactiveError = new("User inactive", "User is not active");
}
