using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.Domain.Entities.Users;

[Owned]
public class Name(string firstname, string lastname)
{
    public string Firstname { get; init; } = firstname;
    public string Lastname { get; init; } = lastname;

    public override string ToString()
    {
        if (string.IsNullOrWhiteSpace(Firstname) && string.IsNullOrWhiteSpace(Lastname))
        {
            return string.Empty;
        }
        return $"{Firstname} {Lastname}";
    }
}
