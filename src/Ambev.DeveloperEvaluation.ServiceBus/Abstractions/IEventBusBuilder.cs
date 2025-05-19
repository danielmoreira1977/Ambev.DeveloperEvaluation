using Microsoft.Extensions.DependencyInjection;

namespace Ambev.DeveloperEvaluation.ServiceBus.Abstractions;

public interface IEventBusBuilder
{
    public IServiceCollection Services { get; }
}
