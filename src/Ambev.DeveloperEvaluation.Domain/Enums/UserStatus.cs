using Ambev.DeveloperEvaluation.Common.Primitives;

namespace Ambev.DeveloperEvaluation.Domain.Enums;

public class UserStatus(int id, string name)
    : Enumeration(id, name)
{
    public static UserStatus Active = new(2, nameof(Active));
    public static UserStatus Inactive = new(3, nameof(Inactive));
    public static UserStatus Suspended = new(4, nameof(Suspended));
    public static UserStatus Unknown = new(1, nameof(Unknown));
}
