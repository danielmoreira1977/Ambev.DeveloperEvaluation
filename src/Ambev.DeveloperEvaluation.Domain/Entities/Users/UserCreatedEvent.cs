using Ambev.DeveloperEvaluation.Common.Primitives.Interfaces;

namespace Ambev.DeveloperEvaluation.Domain.Entities.Users
{
    public record struct UserCreatedEvent(string Email, string FullName) : IDomainEvent;
}
