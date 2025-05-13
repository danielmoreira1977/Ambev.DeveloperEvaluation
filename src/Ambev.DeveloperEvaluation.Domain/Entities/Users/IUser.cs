using Ambev.DeveloperEvaluation.Common.Primitives;
using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Domain.Entities.Users
{
    /// <summary>
    /// Define o contrato para representação de um usuário no sistema.
    /// </summary>
    public interface IUser
    {
        Address Address { get; }
        Email Email { get; }
        UserId Id { get; }
        Name Name { get; }
        Password Password { get; }
        Phone Phone { get; }
        UserRole Role { get; }
        UserStatus Status { get; }
        DateTime? UpdatedAt { get; }
        Username Username { get; }

        void Activate();

        void Deactivate();

        void Suspend();

        ValidationResultDetail Validate();
    }
}
