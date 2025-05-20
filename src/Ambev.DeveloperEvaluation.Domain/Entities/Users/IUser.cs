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
        Name Name { get; }

        Password Password { get; }

        Phone Phone { get; }

        UserRole Role { get; }

        UserStatus Status { get; }

        DateTime? UpdatedAt { get; }

        Username Username { get; }

        void Activate();

        User Create
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
        );

        void Deactivate();

        void SetAddress(Address address);

        void SetEmail(Email email);

        void SetName(Name name);

        void SetPassword(Password password);

        void SetPhone(Phone email);

        void SetRole(UserRole role);

        void SetStatus(UserStatus status);

        void SetUsername(Username username);

        void Suspend();

        ValidationResultDetail Validate();
    }
}
