using Ambev.DeveloperEvaluation.Common.Events;

namespace Ambev.DeveloperEvaluation.ServiceBus.Abstractions;

public interface IEventBus
{
    Task PublishAsync(IntegrationEvent @event);
}
