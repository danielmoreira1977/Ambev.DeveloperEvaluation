using Ardalis.SmartEnum;

namespace Ambev.DeveloperEvaluation.Domain.Enums;

public class UserStatus : SmartEnum<UserStatus>
{
    public static readonly UserStatus Active = new("Active", 2);
    public static readonly UserStatus Inactive = new("Inactive", 3);
    public static readonly UserStatus Suspended = new("Suspended", 4);
    public static readonly UserStatus Unknown = new("Unknown", 1);

    private UserStatus(string name, int value) : base(name, value)
    {
    }
}
