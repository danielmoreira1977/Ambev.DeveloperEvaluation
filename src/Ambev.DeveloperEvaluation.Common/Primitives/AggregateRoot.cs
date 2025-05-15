using Ambev.DeveloperEvaluation.Common.Primitives.Interfaces;

using System.ComponentModel.DataAnnotations.Schema;

namespace Ambev.DeveloperEvaluation.Common.Primitives;

public abstract class AggregateRoot<TId> : BaseEntity<TId>, IAggregateRoot
{
    private readonly List<IDomainEvent> _domainEvents = [];

    [NotMapped]
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public IDomainEvent[] GetAndClearDomainEvents()
    {
        var domainEvents = _domainEvents.ToArray();
        _domainEvents.Clear();
        return domainEvents;
    }

    protected void AddDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);

    protected void RaiseDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);
}
