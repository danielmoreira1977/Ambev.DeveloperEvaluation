using Ambev.DeveloperEvaluation.Common.Primitives;
using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation;

namespace Ambev.DeveloperEvaluation.Domain.Entities.Users;

/// <summary>
/// Represents a user in the system with authentication and profile information. This entity follows
/// domain-driven design principles and includes business rules validation.
/// </summary>
public class User : AggregateRoot<UserId>, IUser
{
    /// <summary>
    /// Used by Ef core.
    /// </summary>
    public User()
    { }

    public User(UserId id, UserStatus status, Username username, UserRole role)
    {
        Id = id;
        SetStatus(status);
        SetUsername(username);
        SetRole(role);
    }

    public Address Address { get; private set; }

    /// <summary>
    /// Gets the date and time when the user was created.
    /// </summary>
    public DateTime CreatedAt { get; init; }

    /// <summary>
    /// Gets the user's email address. Must be a valid email format and is used as a unique
    /// identifier for authentication.
    /// </summary>
    public Email Email { get; private set; }

    public Name Name { get; private set; }

    /// <summary>
    /// Gets the hashed password for authentication. Password must meet security requirements:
    /// minimum 8 characters, at least one uppercase letter, one lowercase letter, one number, and
    /// one special character.
    /// </summary>
    public Password Password { get; private set; }

    /// <summary>
    /// Gets the user's phone number. Must be a valid phone number format following the pattern (XX) XXXXX-XXXX.
    /// </summary>
    public Phone Phone { get; private set; }

    /// <summary>
    /// Gets the user's role in the system. Determines the user's permissions and access levels.
    /// </summary>
    public UserRole Role { get; private set; }

    /// <summary>
    /// Gets the user's current status. Indicates whether the user is active, inactive, or blocked
    /// in the system.
    /// </summary>
    public UserStatus Status { get; protected set; }

    /// <summary>
    /// Gets the date and time of the last update to the user's information.
    /// </summary>
    public DateTime? UpdatedAt { get; private set; }

    /// <summary>
    /// Gets the user's full name. Must not be null or empty and should contain both first and last names.
    /// </summary>
    public Username Username { get; private set; }

    /// <summary>
    /// Activates the user account. Changes the user's status to Active.
    /// </summary>
    public void Activate()
    {
        Status = UserStatus.Active;
        UpdatedAt = DateTime.UtcNow;
    }

    public User Create
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
            )
    {
        var address = new Address(
            city,
            new Geolocation(latitude, longitude),
            number,
            street,
            new ZipCode(zipCode)
         );

        var userEmail = new Email(email);
        var userFullname = new Name(firstname, lastname);

        var user = new User();

        user.SetAddress(address);
        user.SetEmail(userEmail);
        user.SetName(userFullname);
        user.SetPassword(new Password(password));
        user.SetPhone(new Phone(phone));
        user.SetRole(UserRole.FromName(role));
        user.SetStatus(UserStatus.FromName(status));
        user.SetUsername(new Username(username));

        Activate();

        user.AddDomainEvent(new UserCreatedEvent(userEmail.Value, userFullname.ToString()));
        return user;
    }

    /// <summary>
    /// Deactivates the user account. Changes the user's status to Inactive.
    /// </summary>
    public void Deactivate()
    {
        Status = UserStatus.Inactive;
        UpdatedAt = DateTime.UtcNow;
    }

    public void SetAddress(Address address) => Address = address;

    public void SetEmail(Email email) => Email = email;

    public void SetName(Name name) => Name = name;

    public void SetPassword(Password password) => Password = password;

    public void SetPhone(Phone email) => Phone = email;

    public void SetRole(UserRole role) => Role = role;

    public void SetStatus(UserStatus status) => Status = status;

    public void SetUsername(Username username) => Username = username;

    /// <summary>
    /// Blocks the user account. Changes the user's status to Blocked.
    /// </summary>
    public void Suspend()
    {
        Status = UserStatus.Suspended;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Performs validation of the user entity using the UserValidator rules.
    /// </summary>
    /// <returns>
    /// A <see cref="ValidationResultDetail"/> containing:
    /// - IsValid: Indicates whether all validation rules passed
    /// - Errors: Collection of validation errors if any rules failed
    /// </returns>
    /// <remarks>
    /// <listheader>The validation includes checking:</listheader>
    /// <list type="bullet">Username format and length</list>
    /// <list type="bullet">Email format</list>
    /// <list type="bullet">Phone number format</list>
    /// <list type="bullet">Password complexity requirements</list>
    /// <list type="bullet">Role validity</list>
    /// </remarks>
    public ValidationResultDetail Validate()
    {
        var validator = new UserValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}
