namespace Ambev.DeveloperEvaluation.Common.Primitives.Interfaces;

public interface IAggregateRoot
{
    IReadOnlyCollection<IDomainEvent> DomainEvents { get; }

    IDomainEvent[] GetAndClearDomainEvents();
}
