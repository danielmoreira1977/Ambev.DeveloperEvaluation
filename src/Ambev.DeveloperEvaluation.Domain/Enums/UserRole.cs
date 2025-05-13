using Ambev.DeveloperEvaluation.Common.Primitives;

namespace Ambev.DeveloperEvaluation.Domain.Enums;

public class UserRole(int id, string name)
    : Enumeration(id, name)
{
    public static UserRole Admin = new(3, nameof(Admin));
    public static UserRole Customer = new(1, nameof(Customer));
    public static UserRole Manager = new(2, nameof(Manager));
    public static UserRole None = new(1, nameof(None));
}
