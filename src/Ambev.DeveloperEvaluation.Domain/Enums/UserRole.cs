using Ardalis.SmartEnum;

namespace Ambev.DeveloperEvaluation.Domain.Enums;

public class UserRole : SmartEnum<UserRole>
{
    public static readonly UserRole Admin = new("Admin", 2);
    public static readonly UserRole Customer = new("Customer", 3);
    public static readonly UserRole Manager = new("Manager", 4);
    public static readonly UserRole None = new("None", 1);

    private UserRole(string name, int value) : base(name, value)
    {
    }
}
